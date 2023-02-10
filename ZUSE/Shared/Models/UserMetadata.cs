using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZUSE.Shared.Models
{
    public class UserMetadata
    {
        [Key]
        [Required]
        public string phone { get; set; } = null!;
        public string? name { get; set; }
        public int? gender { get; set; }
        public string? birth_date { get; set; }
        public int order_count { get; set; }
        public string? last_order_at { get; set; }
        public string? email { get; set; }
    }
}
