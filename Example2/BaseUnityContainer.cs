using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Example2.Repository;
using Example2.UserMailChecking;
using Example2.UserOnlineChecking;
using Example2.UserOnlineChecking.ResultAdapter;
using Microsoft.Practices.Unity;

namespace Example2
{
    public class BaseUnityContainer : UnityContainer
    {
        public BaseUnityContainer(Default page) : this()
        {
            this.RegisterType<UserMailChecking.IUserInfo, Default.UserMailCheckAdapter.UserInfoProvider>(new InjectionConstructor(page));
            this.RegisterType<IResultHandler<MailRepository.MailInfo>, Default.UserMailCheckAdapter.ResultHandler>
                (new InjectionProperty("Page", page), new InjectionProperty("DataProvider"));

            this.RegisterType<UserOnlineChecking.IUserInfo, Default.UserOnlineCheckAdapter.UserInfoProvider>(new InjectionConstructor(page));
            this.RegisterType<IResultHandler, Default.UserOnlineCheckAdapter.ResultHandler>(new InjectionConstructor(page));
        }

        private BaseUnityContainer()
        {
            this.RegisterType<IMailRepository, MailRepository>();
            this.RegisterType<IUserRepository, UserRepository>();

            this.RegisterType<IUserMailInfoChecker, UserMailInfoChecker>();
            this.RegisterType<IResultAdapter, ResultAdapter>();
            this.RegisterType<IUserStatusChecker, UserStatusChecker>();
        }
    }
}