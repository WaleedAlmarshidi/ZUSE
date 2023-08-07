using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZUSE.Server.Data;
using ZUSE.Server.Mappers;
using ZUSE.Server.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZUSE.Server.Controllers
{
    [Route("shopify")]
    public class ShopifyController : Controller
    {
        // GET: /<controller>/
        private readonly ZUSE_dbContext dbContext;
        private readonly SqaureMapper sessionsMapper;

        public ShopifyController(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;
            this.sessionsMapper = new SqaureMapper();
        }

        //[HttpPost]
        //public IResult NewOrder([FromBody] ShopifyNotification notification)
        //{
            
        //    return Results.Ok();
        //}

        [Route("redirect")]
        [HttpGet]
        public void Redirect()
        {
            var clientId = "aafaed21bb162cde75c922aee3e22356";
            var redirectUri = "https://db0e-51-39-227-16.in.ngrok.io/shopify/redirect";
            var responseType = "code";
            var scopes = "read_orders";
            var queryString = string.Format("client_id={0}&redirect_uri={1}&response_type={2}&scope={3}",
                                                clientId, redirectUri, responseType, scopes);
            var shop = Request.Query["shop"];
            if (Request.Query["code"].Count != 0)
            {
                var code = Request.Query["code"];
                Response.Redirect("https://www.zuse.solutions");
                return;
            }
            var authorizeUrl = $"https://{shop}/admin/oauth/authorize";
            var fullUrl = string.Format("{0}?{1}", authorizeUrl, queryString);
            Response.Redirect(fullUrl);
        }
        [Route("callback")]
        [HttpGet]
        public async Task Callback()
        {
            var grantType = "authorization_code";
            var clientId = "aafaed21bb162cde75c922aee3e22356";
            var clientSecret = "62d4bfb0504eb8d5453478e10bbf6093";
            var redirectUri = "https://9ed0-51-39-228-245.in.ngrok.io/zid/redirect";
            var code = Request.Query["code"];

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", grantType },
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "redirect_uri", redirectUri },
                    { "code", code }
                });

                var response = await client.PostAsync("https://oauth.zid.sa/oauth/token", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Process the response content here...
                //var fullUrl = string.Format("{0}?{1}", authorizeUrl, queryString);
                
                Response.Redirect($"https://wa.me/966532093168?text={responseContent}");

            }
        }
    }
}

