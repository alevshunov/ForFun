using Example2.Bll.UserMailChecking;
using Example2.Bll.UserStatusChecking;
using Example2.Repository;
using Microsoft.Practices.Unity;

namespace Example2.Common
{
    public class BaseUnityContainer : UnityContainer
    {
        protected void RegisterMailChecker(Default page)
        {
            this.RegisterType<Bll.UserMailChecking.IUserInfo, Default.UserMailCheckAdapter.UserInfoProvider>(new InjectionConstructor(page));
            this.RegisterType<IResultHandler<MailRepository.MailInfo>, Default.UserMailCheckAdapter.ResultHandler>
                (new InjectionConstructor(page, new ResolvedParameter<Bll.UserMailChecking.IUserInfo>()));
        }

        protected void RegisterOnlineChecker(Default page)
        {
            this.RegisterType<Bll.UserStatusChecking.IUserInfo, Default.UserStatusCheckAdapter.UserInfoProvider>(new InjectionConstructor(page));
            this.RegisterType<IResultHandler, Default.UserStatusCheckAdapter.ResultHandler>(new InjectionConstructor(page));
        }

        protected void RegisterOnlineChecker(Services.UserStatusChecker.UserStatusContainer.UserStatusModel model, string userInfo)
        {
            this.RegisterType<IResultHandler, Services.UserStatusChecker.UserStatusContainer.ResultAdapter>(new InjectionConstructor(model));
            this.RegisterType<Bll.UserStatusChecking.IUserInfo, Services.UserStatusChecker.UserStatusContainer.UserInfoProvider>(new InjectionConstructor(userInfo));
        }

        public BaseUnityContainer()
        {
            this.RegisterType<IErrorLogger, ErrorLogger>();

            this.RegisterType<IMailRepository, MailRepository>();
            this.RegisterType<IUserRepository, UserRepository>();

            this.RegisterType<IUserMailInfoChecker, UserMailInfoChecker>();
            this.RegisterType<IUserStatusChecker, UserStatusChecker>();
        }
    }
}