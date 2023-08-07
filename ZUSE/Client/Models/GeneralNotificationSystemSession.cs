using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZUSE.Shared.Models;

namespace ZUSE.Client.Models
{
	public class GeneralNotificationSystemSession : Session
	{
		[NotMapped]
		public string displayedNumber { get; set; }
        [NotMapped]
        public string deliveryAppName { get; set; }
	}
}

