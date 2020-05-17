namespace SimpleAuth.Api.Models.PasswordReset
{
    public class ValidatePasswordResetKeyVM
    {
        public string UserName { get; set; }
        public string ResetKey { get; set; }
    }
}
