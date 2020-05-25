namespace SimpleAuth.Common.Entities
{
    public class UserPermission : BaseModel
    {
        public long UserId { get; set; }
        public long PermissionId { get; set; }
    }
}
