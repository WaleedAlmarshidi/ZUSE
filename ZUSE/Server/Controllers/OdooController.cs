using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Server.Mappers;
using ZUSE.Server.Services;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Controllers
{
    [ApiController]
    [Route("odoo")]
    public class OdooController : Controller
    {

        private readonly OdooMapper sessionsMapper;
        private readonly ZUSE_dbContext dbContext;

        public OdooController(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;
            this.sessionsMapper = new OdooMapper();
        }
        public static int MyProperty { get; set; }
        public static string payload { get; set; }
        public static string mappedNotificatoinString { get; set; }
        public static string exception { get; set; }

        [HttpGet]
        public string gettest()
        {
            return MyProperty + " payload: " + payload;
        }
        
        [HttpPost]
        public async Task<IActionResult> test()
        {
            try
            {
                var sessionsManager = new UserSessionsManager(dbContext);
                MyProperty++;

                //HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

                string body = string.Empty;
                using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
                {
                    body = await stream.ReadToEndAsync();
                }
                payload = body;
                var notification = JsonSerializer.Deserialize<OdooOrderNotification>(body);
                var mappedNotification = sessionsMapper.MapOdooNotification(notification);
                mappedNotificatoinString = JsonSerializer.Serialize(mappedNotification);
                await sessionsManager.PostOrUpdateSession(mappedNotification);

            }
            catch (Exception e)
            {
                exception = e.Message ;
            }
            finally
            {
            }
            return Ok("hi, your request was received");

        }
    }

}

