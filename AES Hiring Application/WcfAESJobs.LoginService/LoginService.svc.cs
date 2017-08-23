using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfAESJobs.AccountLibrary;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WcfAESJobs.LoginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoginService.svc or LoginService.svc.cs at the Solution Explorer and start debugging.
    public class LoginService : ILoginService
    {
        private DB_9E4BED_UsersEntities1 database = new DB_9E4BED_UsersEntities1();

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            ApplicationUser user = new ApplicationUser();
            try
            {
                AspNetUser UserEntity = this.database.AspNetUsers.Where(c => c.UserName == userName).FirstOrDefault();
                return Task.FromResult(ConvertUserEntityToAplicationUser(UserEntity));
            }
            catch
            {
                return Task.FromResult(new ApplicationUser{UserName = userName});
            }
            
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            AspNetUser UserEntity = this.database.AspNetUsers.Where(c => c.Id == userId).FirstOrDefault();
            var RolesEntitiy = UserEntity.AspNetRoles;
            ICollection<IdentityUserRole> roles;
            roles = UserEntity.AspNetRoles.Select(x => new IdentityUserRole
            {
                RoleId = x.Id,
                UserId = UserEntity.Id
            }).ToList();
            ApplicationUser user = ConvertUserEntityToAplicationUser(UserEntity);
            return Task.FromResult(user);
        }

        public Task CreateAsync(ApplicationUser user)
        {
            
            this.database.AspNetUsers.Add(ConvertApplicationUserToUserEntity(user));
            return this.database.SaveChangesAsync();
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            AspNetUser UserEntity = this.database.AspNetUsers.Where(c => c.Email == email).FirstOrDefault();
            return Task.FromResult(ConvertUserEntityToAplicationUser(UserEntity));
        }

        public IList<string> GetRolesAsync(ApplicationUser user)
        {
            AspNetUser UserEntity = this.database.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault();
            IList<string> RoleList;
            return RoleList = UserEntity.AspNetRoles.Select(s => s.Name).ToList();
            
        }

        private ApplicationUser ConvertUserEntityToAplicationUser(AspNetUser user)
        {
            if (user == null)
                return null;
            ApplicationUser AppUser = new ApplicationUser();
            AppUser.UserName = user.UserName;
            AppUser.Id = user.Id;
            AppUser.Email = user.Email;
            AppUser.PasswordHash = user.PasswordHash;
            AppUser.SecurityStamp = user.SecurityStamp;
            AppUser.PhoneNumber = user.PhoneNumber;
            return AppUser;
        }
        private AspNetUser ConvertApplicationUserToUserEntity(ApplicationUser user)
        {
            if (user == null)
                return null;
            AspNetUser userEntity = new AspNetUser();
            userEntity.Id = user.Id;
            userEntity.Email = user.Email;
            userEntity.EmailConfirmed = user.EmailConfirmed;
            userEntity.PasswordHash = user.PasswordHash;
            userEntity.SecurityStamp = user.SecurityStamp;
            userEntity.Address1 = user.Address1;
            userEntity.Address2 = user.Address2;
            userEntity.City = user.City;
            userEntity.zip = user.Zip;
            userEntity.State = user.State;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.PhoneNumber = user.PhoneNumber;
            userEntity.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            userEntity.LockoutEnabled = user.LockoutEnabled;
            userEntity.UserName = user.UserName;
                        
            return userEntity;
        }
    }
}
