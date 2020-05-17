using System;

namespace SimpleAuth.Common
{
    public class AuthenticationToken
    {
        public string username { get; set; }
        public string token { get; set; }
        public DateTime? expires { get; set; }
        public bool? islocked { get; set; }
        public string message { get; set; }
        public int? remainingattempts { get; set; }
    }
}
