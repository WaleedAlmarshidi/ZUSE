using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Server.Mappers;
using ZUSE.Server.Models;
using ZUSE.Server.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZUSE.Server.Controllers
{
    [ApiController]
    [Route("loyverse")]
    public class LoyverseController : Controller
    {
        private readonly LoyverseMapper sessionsMapper;
        private readonly ZUSE_dbContext dbContext;
        public static string payload = "none";
        public static int i { get; set; }

        public LoyverseController(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;
            sessionsMapper = new LoyverseMapper();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string Get()
        {
            return payload + i;
        }
        [HttpPost]
        public async Task<IResult> MyProperty([FromBody]LoyverseNotification loyverseNotification)
        {
            i++;

            using var reader = new StreamReader(HttpContext.Request.Body);

            payload = await reader.ReadToEndAsync();

            // As well as a bound model
            var receipt = loyverseNotification.receipts[0];
            var serviceProvider = this.dbContext.ZUSEClients.Where(zc => zc.branch_id.Equals(receipt.store_id)
                                                                                                                &&
                                                                                                                zc.name.Equals(loyverseNotification.merchant_id)).SingleOrDefault();
            if (serviceProvider is null)
                return Results.NotFound("Service provider does not exist");

            var loyverseNotificationMappedToZUSESession = await sessionsMapper.MapLoyverseNotificationAsync(serviceProvider, loyverseNotification);

            var sessionsManager = new UserSessionsManager(dbContext);
            var result = await sessionsManager.PostNewSession(loyverseNotificationMappedToZUSESession);

            return result;
        }
    }
}

