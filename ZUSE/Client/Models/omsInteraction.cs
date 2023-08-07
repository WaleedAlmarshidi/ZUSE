using System;
namespace ZUSE.Client.Models
{
    public interface omsInteraction
    {
        public string Id { get; set; }
        public string nextInteractionId { get; set; }
        public string langDisplayName { get; set; }
        public string categs { get; set; }
		public string recall { get; set; }
		public string summary { get; set; }
		public string pickup { get; set; }
		public string driveT { get; set; }
        public string dineIn { get; set; }
        public string delivery { get; set; }
        public static omsInteraction GetInteractionById(string id)
        {
            //omsInteraction current = new omsEnglish();

            //while (true)
            //{
            //    if (current.langId.Equals(id))
            //        return current;

            //    current = current.nextInteraction;

            //    if (current is omsEnglish)
            //        return new omsEnglish();
            //}

            return new omsEnglish();
        }

    }
	public class omsArabic : omsInteraction
    {
        public string nextInteractionId { get; set; } = "en";
        public string Id { get; set; } = "ar";
        public string categs { get; set; } = "الأصناف";
		public string recall { get; set; } = "السابق";
		public string summary { get; set; } = "الملخص";
		public string pickup { get; set; } = "استلام";
        public string delivery { get; set; } = "توصيل";
        public string dineIn { get; set; } = "محلي";
        public string driveT { get; set; } = "سيارة";
        public string langDisplayName { get; set; } = "عربي";
    }
    public class omsEnglish : omsInteraction
    {
        public string nextInteractionId { get; set; } = "esp";
        public string Id { get; set; } = "en";
        public string langDisplayName { get; set; } = "ENG";
        public string categs { get; set; } = "CATEGS";
        public string recall { get; set; } = "RECALL";
        public string summary { get; set; } = "SUMMARY";
        public string pickup { get; set; } = "PICKUP";
        public string delivery { get; set; } = "DELIVERY";
        public string dineIn { get; set; } = "DINE IN";
        public string driveT { get; set; } = "Drive T";
    }
    public class omsSpanish : omsInteraction
    {
        public string nextInteractionId { get; set; } = "ar";
        public string Id { get; set; } = "esp";
        public string langDisplayName { get; set; } = "ESPAÑOL";
        public string categs { get; set; } = "CATEGS";
        public string recall { get; set; } = "HISTORIAL";
        public string summary { get; set; } = "RESUMEN";
        public string pickup { get; set; } = "LLEVAR";
        public string delivery { get; set; } = "DOMICILIO";
        public string dineIn { get; set; } = "MESAS";
        public string driveT { get; set; } = "AUTO-SERV";
    }
}

