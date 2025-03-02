﻿namespace UserPermissionsApi.Models
{
    public class UserPermission
    {
        public int ID { get; set; }

        public int UserId { get; set; }
        public User ?User { get; set; } 

        public int PermissionId { get; set; }
        public Permission ?Permission { get; set; }
    }
}