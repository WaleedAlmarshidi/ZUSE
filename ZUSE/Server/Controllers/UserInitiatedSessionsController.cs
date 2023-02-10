using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Shared.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZUSE.Server.Controllers
{
    [ApiController]
    [Route("user_sessions")]
    public class UserInitiatedSessionsController : Controller
    {
        private readonly ZUSE_dbContext dbContext;

        public UserInitiatedSessionsController(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IResult> PostNewSession([FromBody] UserInitiatedSession userInitiatedSession)
        {
            var sessionsManager = new UserSessionsManager(dbContext);
            sessionsManager.PostNewUserInitiatedSession(userInitiatedSession);
            return Results.Ok();
        }
    }
}

