using System;

namespace SimpleAuth.Common
{
    public class AuthenticationToken
    {
        public string username { get; set; }
        public string token { get; set; }
        public DateTime expires { get; set; }
        public bool lockstatus { get; set; }
        public string lockmessage { get; set; }
    }
}
