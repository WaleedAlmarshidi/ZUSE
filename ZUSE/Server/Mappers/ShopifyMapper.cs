using System;
using ZUSE.Server.Models;
using ZUSE.Shared.Models;

namespace ZUSE.Server.Mappers
{
	public class ShopifyMapper
	{
		public ShopifyMapper()
		{
		}
		public async Task<Session> MapShopifyNotification(ShopifyNotification shopifyNotification)
		{
			var collections = new List<ProductCollection>();
			//foreach (var item in shopifyNotification.line_items)
			//{
			//	collections.Add(new ProductCollection
			//	{
			//		product = new SingleProduct { name = item.name, category = item. }

			//	});
			//}

			var session = new Session
			{
				created_at = shopifyNotification.created_at,

			};
			return session;
		}
	}
}

