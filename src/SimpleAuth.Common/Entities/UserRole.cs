namespace SimpleAuth.Common.Entities
{
    public class UserRole : BaseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
