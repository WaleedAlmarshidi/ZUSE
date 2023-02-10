using System;
namespace ZUSE.Server.Models
{
	public class Whatsapp_body
	{
		public recipient recipient { get; set; }
		public content content { get; set; }
	}

    public class content
    {
        public string type { get; set; }
		public string name { get; set; }
		public language language { get; set; }
		public List<Component> components { get; set; }
	}

    public class Component
    {
		public string type { get; set; }
		public List<parameter> parameters { get; set; }
	}

    public class parameter
    {
		public string type { get; set; }
		public string text { get; set; }
	}

    public class language
    {
		public string code { get; set; }
	}

    public class recipient
	{
		public string contact { get; set; }
		public string channel { get; set; }
	}
}

