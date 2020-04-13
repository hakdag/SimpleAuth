namespace SimpleAuth.Common
{
    public class AuthorizationResult
    {
        public bool IsAuthorized { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
