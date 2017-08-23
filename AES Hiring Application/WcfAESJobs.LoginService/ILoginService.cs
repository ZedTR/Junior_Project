using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WcfAESJobs.AccountLibrary;

namespace WcfAESJobs.LoginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginService" in both code and config file together.
    
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        Task<ApplicationUser> FindByIdAsync(string userId);

        [OperationContract]
        Task<ApplicationUser> FindByNameAsync(string userName);

        [OperationContract]
        Task CreateAsync(ApplicationUser user);

        [OperationContract]
        Task<ApplicationUser> FindByEmailAsync(string email);

        [OperationContract]
        IList<string> GetRolesAsync(ApplicationUser user);
    }

   
}

    