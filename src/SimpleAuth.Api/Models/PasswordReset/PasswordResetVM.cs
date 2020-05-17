namespace SimpleAuth.Api.Models.PasswordReset
{
    public class PasswordResetVM
    {
        public string UserName { get; set; }
        public string ResetKey { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
