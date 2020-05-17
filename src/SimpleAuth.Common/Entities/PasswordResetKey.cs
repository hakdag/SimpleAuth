namespace SimpleAuth.Common.Entities
{
    public class PasswordResetKey : BaseModel
    {
        public int UserId { get; set; }
        public string ResetKey { get; set; }
    }
}
