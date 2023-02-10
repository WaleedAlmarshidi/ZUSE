using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace ZUSE.Server.Controllers;
using System;
using System.Collections;
using System.Text;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Server.Mappers;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;

[ApiController]
[Route("foodics")]
public class FoodicsController : Controller
{
    private readonly ISessionsMapper sessionsMapper;
    private readonly ZUSE_dbContext dbContext;

    public FoodicsController(ZUSE_dbContext dbContext)
    {
        this.dbContext = dbContext;
        sessionsMapper = new FoodicsMapper();
    }

    public static ICollection<FoodicsOrderNotification>? NotificationData { get; set; }
    public static Session notificationMappedToTathkaraSession { get; private set; }
    public static int NotificationCount { get; set; }
    public static string Exception { get; set; }
    public static string response { get; set; }
    public static string payload { get; set; }
    public static string localPayload { get; set; }

    public IResult result { get; private set; }

    [HttpPost]
    public async Task<IResult> PostOrder([FromBody] FoodicsOrderNotification foodicsOrderNotification)
    {
        try
        {
            NotificationCount++;
            //payload = JsonSerializer.Serialize(foodicsOrderNotification);
            //var foodicsOrderNotification = JsonSerializer.Deserialize<FoodicsOrderNotification>(payload);
            if(foodicsOrderNotification is null)
            {
                Console.WriteLine("order notification in foodics controller is null");
                return null;
            }
            //if (foodicsOrderNotification.order.status != 4)
            //    return Results.Ok("Order status not accepted");
            notificationMappedToTathkaraSession = sessionsMapper.MapFoodicsNotification(foodicsOrderNotification);

            var sessionsManager = new UserSessionsManager(dbContext);
            result = await sessionsManager.PostNewSession(notificationMappedToTathkaraSession);

            return result;
        }
        catch (Exception ex)
        {
            Exception = ex.ToString();
            return Results.Ok(Exception);
        }
    }

    [HttpPost("local_order")]
    public async Task<IResult> localOrder()
    {
        try
        {
            string payload = string.Empty;
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                payload = await stream.ReadToEndAsync();
            }
            localPayload = payload;
            var x = JsonSerializer.Deserialize<FoodicsLocalNetworkOrder>(payload);
            var y = mapLocalOrderToNotification(x);

            notificationMappedToTathkaraSession = sessionsMapper.MapFoodicsNotification(y);

            var sessionsManager = new UserSessionsManager(dbContext);
            result = await sessionsManager.PostOrUpdateSession(notificationMappedToTathkaraSession);

            return result;
        }
        catch(Exception e)
        {
            return Results.Problem(e.Message);
        }
    }
    [HttpGet("local")]
    public string GetLatestLocal()
    {
        return localPayload;
    }
    [HttpGet]
    public string GetLatestOrder()
    {
        return 
            $"count: {NotificationCount}\n\n\n\n notification: {payload}";
    }
    private FoodicsOrderNotification mapLocalOrderToNotification(FoodicsLocalNetworkOrder foodicsLocalNetworkOrder)
    {
        var frnProducts = new List<ProductCollection>();
        
        foreach (var item in foodicsLocalNetworkOrder.order.products)
        {
            if (item.status == 5)
                continue;

            var options = new List<SingleProductOptions>();
            foreach (var option in item.options)
            {
                options.Add(new SingleProductOptions
                {
                    modifier_option = new SingleProductModifierOption
                    {
                        name = option.option.name,
                        quantity = option.quantity,
                        name_localized = option.option.name_localized
                    }
                });
            }
            frnProducts.Add(new ProductCollection
            {
                product = new SingleProduct
                {
                    name = item.product.name,
                    category = item.product.category
                },
                quantity = item.quantity,
                kitchen_notes = item.notes,
                options = options,
                //stage = item.status == 5 ? productMarks.removed : item.status == 6 ? productMarks.added : productMarks.normal
            });
        }
        var frn = new FoodicsOrderNotification
        {
            order = new OrderInfo
            {
                id = foodicsLocalNetworkOrder.order.uuid.ToLower(),
                number = foodicsLocalNetworkOrder.order.number,
                created_at = foodicsLocalNetworkOrder.order.opened_at.Subtract(TimeSpan.FromHours(3)),
                products = frnProducts,
                combos = new List<ComboCollection>(),
                type = foodicsLocalNetworkOrder.order.type,
                source = 1,
                delivery_status = 1,
                kitchen_notes = foodicsLocalNetworkOrder.order.notes,
                customer_notes = "customer_notes",
                branch = new FoodicsBranch {
                    id = foodicsLocalNetworkOrder.source.branch_id
                }
            },
            @event = "order.created",
            business = new Business
            {
                name = foodicsLocalNetworkOrder.order.user.name,
                reference = int.Parse(foodicsLocalNetworkOrder.source.business_reference)
            }
        };
        if (foodicsLocalNetworkOrder.order.table is not null)
            frn.order.table = new Table { name = foodicsLocalNetworkOrder.order.table?.section.name + " " + foodicsLocalNetworkOrder.order.table?.name };
        return frn;
    }
}

