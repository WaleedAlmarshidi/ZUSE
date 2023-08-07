using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZUSE.Server.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZUSE.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        [Route("zid-redirect")]
        public void Redirect()
        {
            var clientId = "1770";
            var redirectUri = "https://client.zuse.solutions/zid/oauth/callback";
            var responseType = "code";
            var queryString = string.Format("client_id={0}&redirect_uri={1}&response_type={2}",
                                             clientId, redirectUri, responseType);
            var authorizeUrl = "https://oauth.zid.sa/oauth/authorize";
            var fullUrl = string.Format("{0}?{1}", authorizeUrl, queryString);
            Response.Redirect(fullUrl);
        }

        // GET: /<controller>/
        [HttpPost("ping")]
        public async Task<ActionResult> Index([FromBody] RequestOfFoodicsLocalConnectionPing x)
        {
            //var request = Request;
            //string text;
            //using (var reader = new StreamReader(request.Body))
            //{
            //    text = await reader.ReadToEndAsync();
            //}
            //Console.WriteLine(text);
            //var x = JsonSerializer.Deserialize<RequestOfFoodicsLocalConnectionPing>(text);
            var response = new ResponseToFoodicsPing
            {
                data = new ResponseOfFoodicsLocalConnectionPing
                {
                    source = x.source,
                    target = new ResponseTarget
                    {
                        branch_id = x.source.branch_id,
                        business_reference = x.source.business_reference,
                        system_version = x.source.system_version,
                        ip_address = x.target.ip_address,
                        uuid = x.target.uuid,
                        id = x.target.id,
                        type = x.target.type,
                        name = x.target.name
                    }
                }
            };
            return Content(JsonSerializer.Serialize(response), "application/json");
        }
    }
}

