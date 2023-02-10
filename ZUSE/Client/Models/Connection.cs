using System;
namespace ZUSE.Client.Models
{
	public class Ping
	{
		public int id { get; set; }
	}
	public class ResponseToPing
	{
		public int responderId { get; set; }
		public bool isActive { get; set; }
	}
}

