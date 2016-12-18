using Microsoft.AspNet.Identity;
using MyPhotoViewer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhotoViewer.Extensions
{
    public static class UserManagerExtensions
    {
        public static ApplicationUser FindByNumber(this ApplicationUserManager userManager, int number)
        {
            return userManager.Users.Single(user => user.Number == number);
        }

        public static async Task<IEnumerable<IdentityResult>> UpdateUserAsync(this ApplicationUserManager userManager, ApplicationUser user, string[] roleNames)
        {
            var results = new List<IdentityResult>();

            results.Add(await userManager.UpdateAsync(user));

            IEnumerable<string> currentRoleNames = await userManager.GetRolesAsync(user.Id);

            IEnumerable<string> addedRoleNames = roleNames.Except(currentRoleNames);
            results.Add(await userManager.AddToRolesAsync(user.Id, addedRoleNames.ToArray()));

            IEnumerable<string> removedRoleNames = currentRoleNames.Except(roleNames);
            results.Add(await userManager.RemoveFromRolesAsync(user.Id, removedRoleNames.ToArray()));

            return results;
        }
    }
}