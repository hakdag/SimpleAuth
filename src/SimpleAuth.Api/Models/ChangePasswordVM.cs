namespace SimpleAuth.Api.Models
{
    public class ChangePasswordVM
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
