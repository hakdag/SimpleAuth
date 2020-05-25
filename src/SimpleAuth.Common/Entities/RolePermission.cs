namespace SimpleAuth.Common.Entities
{
    public class RolePermission : BaseModel
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
