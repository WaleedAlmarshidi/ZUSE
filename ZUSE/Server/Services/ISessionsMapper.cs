using System;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;
using ZUSE.Server.Models;

namespace ZUSE.Server.Services
{
    public interface ISessionsMapper
    {
       public Session MapFoodicsNotification(FoodicsOrderNotification notification);
        public Session MapOdooNotification(OdooOrderNotification orderNotification);
        public Task<Session> MapLoyverseNotificationAsync(ZUSEClient serviceProvider, LoyverseNotification loyverseNotification);
        public Task<Session> MapSqaureNotificationAsync(SqaureNotification sqaureNotification);
    }
}

