using System;
using ZUSE.Shared.Models;

namespace ZUSE.Server.Data
{
    public interface IUsersManager
    {
        public Task AddWebHookInitiatedSession(ZUSE_dbContext dbContext, Session managedSession, ZUSEClient serviceProvider);
        public IResult AddNewServiceProvider(ZUSEClient serviceProvider);
        public Task<IResult> PostNewSession(Session userSession);
        public Task<IResult> PushClosedSession(Session session);
        public void PostNewUserInitiatedSession(UserInitiatedSession userInitiatedSession);
    }
}