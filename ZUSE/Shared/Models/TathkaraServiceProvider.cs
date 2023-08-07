using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZUSE.Shared.Models
{
    public enum service_type
    {
        resturant, coffee, hospital, car_repair,
        clinic, bank, gov
    }
    public enum integrated_pos
    {
        odoo, foodics, loyverse, square
    }
    public class ZUSEClient
    {
        
        public string topic { get; set; } = null!;

        [Key]
        public string reference_id { get; set; } = null!;
        [Key]
        public string branch_id { get; set; } = null!;

        public string? name { get; set; }
        public service_type business_type { get; set; }
        public bool is_pos_integrated { get; set; }
        public integrated_pos integrated_pos { get; set; }
        public string base_pos_provider_api_url { get; set; }
        public string pos_categories_fetch_url { get; set; }
        public string? sections_fetch_url { get; set; }
        public string tables_fetch_url { get; set; }
        public bool is_tv_provider { get; set; }
        public bool is_kds_provider { get; set; }
        public bool is_mobile_notifier_provider { get; set; }
        public bool is_loyalty_enabled { get; set; }
        public bool is_external_notification_user { get; set; }
        public string? access_token { get; set; }
        public string? external_notification_name { get; set; }
        public int? external_notification_limit { get; set; }
        public int? external_notification_count { get; set; }
        public string? external_notification_special_msg { get; set; }
        public string? rewards_plan { get; set; }
        public double? rewards_points_threshold { get; set; }
        public string? rewards_greeting { get; set; }
        public string? tv_text_to_speach { get; set; }
        public bool is_kds_order_completion_approval_needed { get; set; } = false;
    }
}
