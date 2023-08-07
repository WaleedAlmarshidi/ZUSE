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
    [Route("sqaure")]
    public class SqaureController : Controller
    {
        private readonly ZUSE_dbContext dbContext;
        private readonly SqaureMapper sessionsMapper;

        public SqaureController(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;
            this.sessionsMapper = new SqaureMapper();
        }
        [HttpPost]
        public async Task<IResult> orderCreated( [FromBody] SqaureNotification sqaureNotification)
        {
            var session = await this.sessionsMapper.MapSqaureNotification(sqaureNotification);
            var manager = new UserSessionsManager(this.dbContext);
            return await manager.PostOrUpdateSession(session);
        }
    }
}

