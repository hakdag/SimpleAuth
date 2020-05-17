namespace SimpleAuth.Api.Models
{
    public class ValidatePasswordResetKeyVM
    {
        public string UserName { get; set; }
        public string ResetKey { get; set; }
    }
}
