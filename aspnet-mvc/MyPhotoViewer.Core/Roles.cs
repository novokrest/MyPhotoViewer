using System;

namespace MyPhotoViewer.Core
{
    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";

        public const string UserAndAdmin = User + "," + Admin;


        public static string ListRoles(params string[] roles)
        {
            return string.Join(",", roles);
        }
    }
}
