using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPhotoViewer;
using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Entity;
using MyPhotoViewer.Models;
using PhotoDiscoverService.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhotoDiscoverService
{
    internal static class DatabaseInitializer
    {
        public static void Initialize<TDbContext, TDbInitializer>() 
            where TDbContext : DbContext 
            where TDbInitializer : IDatabaseInitializer<TDbContext>
        {
            var dbInitializer = Activator.CreateInstance<TDbInitializer>();
            Database.SetInitializer(dbInitializer);

            using (var dbContext = Activator.CreateInstance<TDbContext>())
            {
                dbContext.Database.Initialize(force: true);
                dbContext.SaveChanges();
            }
        }
    }

    internal class ForceDropCreateDatabaseAlways<TDbContext> : DropCreateDatabaseAlways<TDbContext>
        where TDbContext : DbContext
    {
        private const string SetSingleUserSqlCommand = "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
        private const string DropDatatbaseSqlCommand = "USE master DROP DATABASE [{0}]";

        public override void InitializeDatabase(TDbContext context)
        {
            if (context.Database.Exists())
            {
                string databaseName = context.Database.Connection.Database;
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format(SetSingleUserSqlCommand, databaseName));
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format(DropDatatbaseSqlCommand, databaseName));
            }

            base.InitializeDatabase(context);
        }
    }

    internal class PhotosDbInitializer : ForceDropCreateDatabaseAlways<PhotosDbContext>
    {
        protected override void Seed(PhotosDbContext context)
        {
            base.Seed(context);

            var photoAlbumsLoader = PhotoAlbumsLoader.CreateFromConfiguration(context);
            photoAlbumsLoader.LoadPhotoAlbums();
        }
    }

    internal class ApplicationDbInitializer : ForceDropCreateDatabaseAlways<ApplicationDbContext>
    {
        private const string AdminRoleName = "Admin";
        private const string UserRoleName = "User";
        private const string AdminEmail = "admin@mail.com";
        private const string UserEmail = "user@mail.com";

        private readonly AdminPasswordProvider _adminPasswordProvider = new AdminPasswordProvider();

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            using (var userCreator = new ApplicationUserCreator(context))
            {
                string adminPassword = _adminPasswordProvider.GetAdminPassword();
                userCreator.Create(AdminEmail, adminPassword, AdminEmail, AdminRoleName, UserRoleName);

                userCreator.Create(UserEmail, "123456", UserEmail, UserRoleName);
            }
        }

        private sealed class ApplicationRoleCreator: IDisposable
        {
            private readonly ISet<string> _createdRoles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            private readonly RoleManager<IdentityRole> _roleManager;

            public ApplicationRoleCreator(ApplicationDbContext context)
            {
                _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                LoadRoles();
            }

            private void LoadRoles()
            {
                _createdRoles.AddRange(_roleManager.Roles.Select(role => role.Name));
            }
            
            public void EnsureRole(string roleName)
            {
                if (!IsRoleCreated(roleName))
                {
                    CreateRole(roleName);
                }
            }

            private bool IsRoleCreated(string roleName)
            {
                return _createdRoles.Contains(roleName);
            }

            private void CreateRole(string roleName)
            {
                var role = new IdentityRole { Name = roleName };
                var result = _roleManager.Create(role);
                Verifiers.Verify(result.Succeeded, "Failed to create role: {0}. Errors: {1}", roleName, result.Errors.ToJoinString());
                _createdRoles.Add(role.Name);
            }

            public void Dispose()
            {
                _roleManager?.Dispose();
            }
        }

        private sealed class ApplicationUserCreator : IDisposable
        {
            private readonly ApplicationUserManager _userManager;
            private readonly ApplicationRoleCreator _roleCreator;

            public ApplicationUserCreator(ApplicationDbContext context)
            {
                _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                _roleCreator = new ApplicationRoleCreator(context);
            }

            public void Create(string username, string password, string email, params string[] roles)
            {
                var user = new ApplicationUser { UserName = username, Email = email };
                Create(user, password, roles);
            }

            private void Create(ApplicationUser user, string password, params string[] roles)
            {
                var result = _userManager.Create(user, password);
                Verifiers.Verify(result.Succeeded, "Failed to create user: {0}. Errors: {1}", user.UserName, result.Errors.ToJoinString());

                roles.ForEach(_roleCreator.EnsureRole);
                result = _userManager.AddToRoles(user.Id, roles);
                Verifiers.Verify(result.Succeeded, "Failed to add user '{0}' to roles '{1}'. Errors: {2}", user.UserName, roles, result.Errors.ToJoinString());
            }

            public void Dispose()
            {
                _userManager?.Dispose();
                _roleCreator?.Dispose();
            }
        }
    }
}
