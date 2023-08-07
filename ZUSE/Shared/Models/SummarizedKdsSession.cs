using System;
using ZUSE.Shared.Models;

namespace ZUSE.Shared.Models
{
	public class SummerizedKdsSessions
	{
		public List<SingleSummarizedKdsCollection> sessions { get; set; } = new();
		public Dictionary<string, int> orderTypeQuantitty { get; set; }
		public IEnumerable<IGrouping<string,  SingleSummarizedKdsCollection>> groupedSessions { get; set; }
	}
	public class SingleSummarizedKdsCollection
    {
		public string productName { get; set; }
        public int quantity { get; set; }
		public string? kitchen_notes { get; set; }
		public List<SingleProductOptions>? options { get; set; }
	}
}

