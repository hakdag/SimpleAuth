﻿namespace SimpleAuth.Common.Entities
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
