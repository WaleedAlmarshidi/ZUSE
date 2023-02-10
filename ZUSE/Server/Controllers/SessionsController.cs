using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ZUSE.Shared.Models;
using ZUSE.Server.Managers;
using ZUSE.Server.Data;
using System.Net;
using ZUSE.Shared.Models.PosModels;
using Microsoft.AspNetCore.Authorization;

namespace ZUSE.Server.Controllers;

[ApiController]
[Route("sessions")]
public class SessionsController : ControllerBase
{
    private readonly IUsersManager sessionsManager;
    private readonly ZUSE_dbContext dbContext;

    public static Session notification { get; set; }

    public SessionsController(ZUSE_dbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    
    private bool OrderHasPassed12Hours(IQueryable<Session> serviceProviderSessions, string orderReference)
    {
       return false;
    }

    //[HttpPost]
    //public IResult PostNewSession([FromBody] UserSession newSession)
    //{
    //    try
    //    {

    //        notification = newSession;
            
    //    }
    //    catch (Exception e) 
    //    {
    //        try
    //        {
    //            throw;
    //        }
    //        catch
    //        {
    //            return Results.Problem(e.ToString());
    //        }
    //    }
    //}

    //    [HttpGet("sp")]
    //    public IActionResult GetSpReward(string sp)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        return Ok(dbContext.Sessions.Where(
    //                session => session.ServiceProvider.Equals(sp)
    //            ));
    //    }
    [HttpDelete]
    [Authorize]
    public IActionResult DeleteAllSessions()
    {
        foreach (var item in dbContext.sessions)
        {
            dbContext.sessions.Remove(item);
        }
        dbContext.SaveChanges();
        return Ok(
            );
    }

    //    [HttpGet]
    //    public IActionResult GetSingleReward(string or,
    //        string sp, DateTime date)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        var existings_sessoin = dbContext.Sessions.Where(
    //                session => session.OrderReference.Equals(or) &&
    //                session.ServiceProvider.Equals(sp) &&
    //                session.OrderDate.Equals(date)
    //            ).SingleOrDefault();

    //        return existings_sessoin is not null ? Ok(existings_sessoin) :
    //            NotFound("Session not found");

    //    }
    //    [HttpGet("SingleCustomerRetention")]
    //    public IActionResult GetSingleCustomerRetention(string sp, string pn)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        return Ok(dbContext.Sessions.Count(
    //                session =>
    //                    session.ServiceProvider.Equals(sp) &&
    //                    session.PhoneNumber.Equals(pn)
    //                ));
    //    }

    //    [HttpPut("PhoneNumber/{or}/{sp}/{date}")]
    //    public async Task<IActionResult> PutSessionPhoneNumber(string or, string sp, DateTime date,
    //        string PhoneNumber)
    //    {
    //        var dbContext = new tathkara_dbContext();
    //        var existing_session = dbContext.Sessions.Where(
    //                session => session.OrderReference.Equals(or) &&
    //                session.ServiceProvider.Equals(sp) &&
    //                session.OrderDate.Equals(date)
    //            ).SingleOrDefault();

    //        if (existing_session is null)
    //            return NotFound("Not found");

    //        existing_session.PhoneNumber = PhoneNumber;
    //        dbContext.SaveChanges();

    //        return Accepted(GetSingleCustomerRetention(sp, PhoneNumber));
    //    }
    //[HttpGet()]


    [HttpPatch("session_products/{id}/{businessName}")]
    public IActionResult PutSessionProducts(string id, string businessName, [FromBody] ProductsCombosSet productsCombosSet)
    {
        try
        {
            //var userSession = JsonSerializer.Deserialize<UserSession>(session);
            var existingSession = dbContext.sessions.Find(id, businessName);
            if (existingSession is null)
                return NotFound();
            existingSession.products = productsCombosSet.products;
            existingSession.combos = productsCombosSet.combos;

            dbContext.SaveChanges();
            return Ok("record updated");
        }
        catch (Exception e )
        {
            Console.WriteLine("error in PutSessionProducts : " + e.Message);
            return Problem("exception found : " + e.Message);
        }
    }

    [HttpPut("close_session")]
    public async Task<IResult> PutOrderDeliveryStatus()
    {
        try
        {
            var payload = string.Empty;
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                payload = await stream.ReadToEndAsync();
            }
            var session = JsonSerializer.Deserialize<Session>(payload);
            var sessionsManager = new UserSessionsManager(dbContext);
            return await sessionsManager.PushClosedSession(session);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception in PutOrderDeliveryStatus: " + e.Message + " trace: " + e.StackTrace);
            return Results.Problem();
        }
    }
}

