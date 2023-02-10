using System;
namespace ZUSE.Shared.Models
{
    public class PushSubsicribtion
    {
        public PushSubsicribtion()
        {
        }

        public PushSubsicribtion(string endpoint, string p256dh, string auth)
        {
            Endpoint = endpoint;
            P256DH = p256dh;
            Auth = auth;
        }

        public string Endpoint { get; set; } = null!;
        public string P256DH { get; set; } = null!;
        public string Auth { get; set; } = null!;
    }
}

