﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfAESJobs.Client.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserService.ILoginService")]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindById", ReplyAction="http://tempuri.org/ILoginService/FindByIdResponse")]
        WcfAESJobs.AccountLibrary.ApplicationUser FindById(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindById", ReplyAction="http://tempuri.org/ILoginService/FindByIdResponse")]
        System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByIdAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindByName", ReplyAction="http://tempuri.org/ILoginService/FindByNameResponse")]
        WcfAESJobs.AccountLibrary.ApplicationUser FindByName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindByName", ReplyAction="http://tempuri.org/ILoginService/FindByNameResponse")]
        System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByNameAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Create", ReplyAction="http://tempuri.org/ILoginService/CreateResponse")]
        void Create(WcfAESJobs.AccountLibrary.ApplicationUser user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Create", ReplyAction="http://tempuri.org/ILoginService/CreateResponse")]
        System.Threading.Tasks.Task CreateAsync(WcfAESJobs.AccountLibrary.ApplicationUser user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindByEmail", ReplyAction="http://tempuri.org/ILoginService/FindByEmailResponse")]
        WcfAESJobs.AccountLibrary.ApplicationUser FindByEmail(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/FindByEmail", ReplyAction="http://tempuri.org/ILoginService/FindByEmailResponse")]
        System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByEmailAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetRolesAsync", ReplyAction="http://tempuri.org/ILoginService/GetRolesAsyncResponse")]
        string[] GetRolesAsync(WcfAESJobs.AccountLibrary.ApplicationUser user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/GetRolesAsync", ReplyAction="http://tempuri.org/ILoginService/GetRolesAsyncResponse")]
        System.Threading.Tasks.Task<string[]> GetRolesAsyncAsync(WcfAESJobs.AccountLibrary.ApplicationUser user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : WcfAESJobs.Client.UserService.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.ClientBase<WcfAESJobs.Client.UserService.ILoginService>, WcfAESJobs.Client.UserService.ILoginService {
        
        public LoginServiceClient() {
        }
        
        public LoginServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WcfAESJobs.AccountLibrary.ApplicationUser FindById(string userId) {
            return base.Channel.FindById(userId);
        }
        
        public System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByIdAsync(string userId) {
            return base.Channel.FindByIdAsync(userId);
        }
        
        public WcfAESJobs.AccountLibrary.ApplicationUser FindByName(string userName) {
            return base.Channel.FindByName(userName);
        }
        
        public System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByNameAsync(string userName) {
            return base.Channel.FindByNameAsync(userName);
        }
        
        public void Create(WcfAESJobs.AccountLibrary.ApplicationUser user) {
            base.Channel.Create(user);
        }
        
        public System.Threading.Tasks.Task CreateAsync(WcfAESJobs.AccountLibrary.ApplicationUser user) {
            return base.Channel.CreateAsync(user);
        }
        
        public WcfAESJobs.AccountLibrary.ApplicationUser FindByEmail(string email) {
            return base.Channel.FindByEmail(email);
        }
        
        public System.Threading.Tasks.Task<WcfAESJobs.AccountLibrary.ApplicationUser> FindByEmailAsync(string email) {
            return base.Channel.FindByEmailAsync(email);
        }
        
        public string[] GetRolesAsync(WcfAESJobs.AccountLibrary.ApplicationUser user) {
            return base.Channel.GetRolesAsync(user);
        }
        
        public System.Threading.Tasks.Task<string[]> GetRolesAsyncAsync(WcfAESJobs.AccountLibrary.ApplicationUser user) {
            return base.Channel.GetRolesAsyncAsync(user);
        }
    }
}
