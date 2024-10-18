using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // Models/AuthResponse.cs
    public class AuthResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }

    public class LoginResponse
    {
        public int UserMasterId { get; set; }
        public string? Name { get; set; }
        public string? EmailID { get; set; }
        public List<Role>? UserRoles { get; set; }

        public List<RolePermissions>? UserRolePermissions { get; set; }

        public int RoleId { get; set; }

        public string? Token { get; set; }
        public string? BusinessOwner { get; set; }
        public int BusinessDivisionId { get; set; }
        public int Division { get; set; }
        public DateTime TokenExpiration { get; set; }

        public bool? IsSuperUser { get; set; }

    }

    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

    }

    public class RolePermissions
    {
        public int RolePrivilegeId { get; set; }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public int PrivilegeId { get; set; }

        public string? PrivilegeName { get; set; }

        public int SubModuleId { get; set; }

        public string? SubModuleName { get; set; }

        public string? AppModuleName { get; set; }

        public int AppModuleId { get; set; }

        public bool PermissionStatus { get; set; }

    }
}
