using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZUSE.Server.Controllers
{
    [Route("zid")]
    public class ZidController : Controller
    {
        // GET: /<controller>/
        [Route("redirect")]
        [HttpGet]
        public void Redirect()
        {
            var clientId = "1770";
            var redirectUri = "https://9ed0-51-39-228-245.in.ngrok.io/zid/callback";
            var responseType = "code";
            var queryString = string.Format("client_id={0}&redirect_uri={1}&response_type={2}",
                                                clientId, redirectUri, responseType);
            var authorizeUrl = "https://oauth.zid.sa/oauth/authorize";
            var fullUrl = string.Format("{0}?{1}", authorizeUrl, queryString);
            Response.Redirect(fullUrl);
        }
        [Route("callback")]
        [HttpGet]
        public async Task Callback()
        {
            var grantType = "authorization_code";
            var clientId = 1770;
            var clientSecret = "ELEQcOjrjxtJWl6XTC7Q4kX6He1OLC9tcHiP7OMt";
            var redirectUri = "https://9ed0-51-39-228-245.in.ngrok.io/zid/redirect";
            var code = Request.Query["code"];

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", grantType },
                    { "client_id", clientId.ToString() },
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

