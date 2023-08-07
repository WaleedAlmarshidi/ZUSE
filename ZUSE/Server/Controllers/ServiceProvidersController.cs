using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Server.Models;
using ZUSE.Shared.Models;
namespace ZUSE.Server.Controllers;

[ApiController]
[Route("service_providers")]
public class SPsController : ControllerBase
{
    private readonly IUsersManager sessionsManager;
    private readonly ZUSE_dbContext dbContext;

    public SPsController(ZUSE_dbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("all")]
    [Authorize]
    public IActionResult GetSPInfo()
    {
        return Ok(dbContext.ZUSEClients.ToList());
    }
    [HttpGet("{topic}")]
    public IActionResult getKdsServiceOptinos(string topic)
    {
        var existingServiceProvider = dbContext.ZUSEClients.Where(zc => zc.topic.Equals(topic)).FirstOrDefault();
        //return JsonSerializer.Serialize(dbContext.KdsServiceOptions.Find(existingServiceProvider.kds_service_options.id));
        if (existingServiceProvider is null)
            return NotFound();
        return Ok(existingServiceProvider);
    }
    [HttpGet("categories/{topic}")]
    public async Task<options> productsCategories(string topic)
    {
        try
        {
            var existingServiceProvider = dbContext.ZUSEClients.Where(serviceProvider => serviceProvider.topic.Equals(topic)).FirstOrDefault();

            if (existingServiceProvider is null)
                return null;

            switch(existingServiceProvider.integrated_pos)
            {
                case integrated_pos.loyverse:
                    return await Loyverse.GetCategsListAsync(existingServiceProvider);

                case integrated_pos.square:
                    return await Sqaure.GetCategsListAsync();
            }
            var client = new HttpClient();
            HttpRequestMessage requestMessage;

            byte pageNumber = 1;
            HttpResponseMessage result;
            options response = new();
            options newResponse = new();
            Uri uri;
            string modifiedUri = existingServiceProvider.pos_categories_fetch_url;
            bool integratedPosNeedsPagination = existingServiceProvider.integrated_pos == integrated_pos.foodics;

            if (integratedPosNeedsPagination)
                modifiedUri += "&page=";

            do
            {
                if (integratedPosNeedsPagination)
                    uri = new Uri(modifiedUri + pageNumber);
                else
                    uri = new Uri(modifiedUri);

                requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {existingServiceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };
                result = await client.SendAsync(requestMessage);
                newResponse = await result?.Content?.ReadFromJsonAsync<options?>();
                response.data.AddRange(newResponse?.data);
                pageNumber++;
            }
            while (newResponse?.links?.next is not null);

            return response;
        }
        catch(Exception ex)
        {

        }
        return new();
    }
    [HttpGet("tables/{topic}")]
    public async Task<options> getClientTables(string topic)
    {

        try
        {
            var existingServiceProvider = dbContext.ZUSEClients.Where(serviceProvider => serviceProvider.topic.Equals(topic)).FirstOrDefault();

            if (existingServiceProvider is null)
                return null;

            var client = new HttpClient();
            HttpRequestMessage requestMessage;

            byte pageNumber = 1;
            HttpResponseMessage result;
            options response = new();
            options newResponse = new();
            Uri uri;
            string modifiedUri = existingServiceProvider.tables_fetch_url;
            if (existingServiceProvider.integrated_pos == integrated_pos.foodics)
                modifiedUri += "&page=";

            do
            {
                uri = new Uri(modifiedUri + pageNumber);
                requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {existingServiceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };
                result = await client.SendAsync(requestMessage);
                newResponse = await result?.Content?.ReadFromJsonAsync<options?>();
                response.data.AddRange(newResponse?.data);
                pageNumber++;
            }
            while (newResponse?.links?.next is not null);

            return response;
        }
        catch
        {

        }
        return new();
    }
    [HttpGet("sections/{topic}")]
    public async Task<string> getClientSections(string topic)
    {
        try
        {
            var existingServiceProvider = dbContext.ZUSEClients.Where(serviceProvider => serviceProvider.topic.Equals(topic)).FirstOrDefault();

            if (existingServiceProvider is null || existingServiceProvider.sections_fetch_url is null)
                return string.Empty;

            using (var client = new HttpClient())
            {

                var foodicsRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(existingServiceProvider.sections_fetch_url),
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {existingServiceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };

                var result = await client.SendAsync(foodicsRequestMessage);
                var response = await result.Content.ReadAsStringAsync();
                return response;
            }
        }
        catch
        {

        }
        return string.Empty;
    }
    [HttpPost]
    public IResult PostNewSP([FromBody] ZUSEClient newServiceProvider)
    {
        var sessionsManager = new UserSessionsManager(dbContext);
        return sessionsManager.AddNewServiceProvider(newServiceProvider);
    }

    [HttpDelete]
    [Authorize]
    public IResult DeleteAll()
    {
        dbContext.ZUSEClients.RemoveRange(dbContext.ZUSEClients.ToList());
        dbContext.SaveChanges();
        return Results.Ok();
    }

    //    [HttpPut("{ID}")]
    //    public IActionResult PutSP(string? ID, [FromQuery] Sp sp)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        if (dbContext.Sps.Where(
    //                sp => sp.Topic.Equals(ID)
    //            ).SingleOrDefault() is not Sp existed_sp)
    //            return NotFound("not found");
    //        existed_sp.Topic = sp.Topic;
    //        existed_sp.BusinessType = sp.BusinessType;
    //        existed_sp.TvTts = sp.TvTts;
    //        existed_sp.ExternalNotificationCount = sp.ExternalNotificationCount;
    //        existed_sp.ExternalNotificationLimit = sp.ExternalNotificationLimit;
    //        existed_sp.IsExternalNotificationUser = sp.IsExternalNotificationUser;
    //        existed_sp.IsTvProvider = sp.IsTvProvider;
    //        existed_sp.IsRewardsSystemUser = sp.IsRewardsSystemUser;
    //        existed_sp.RewardsPlan = sp.RewardsPlan;
    //        existed_sp.RewardsPointsThreshold = sp.RewardsPointsThreshold;
    //        existed_sp.ExternalNotificationName = sp.ExternalNotificationName;
    //        existed_sp.ClientId = sp.ClientId;
    //        dbContext.SaveChanges();
    //        return Accepted();
    //    }
    //    [HttpPut("ExternalNotificationCount/{ID}")]
    //    public IActionResult PutSP(string? ID, int ExternalNotificationCount)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        if (dbContext.Sps.Where(
    //                sp => sp.Topic.Equals(ID)
    //            ).SingleOrDefault() is not Sp existed_sp)
    //            return NotFound("not found");
    //        existed_sp.ExternalNotificationCount = ExternalNotificationCount;
    //        dbContext.SaveChanges();
    //        return Accepted();
    //}
}

