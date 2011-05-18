using System;
using Example2.Bll.UserStatusChecking;
using Example2.Common;
using Microsoft.Practices.Unity;

namespace Example2.Services
{
    public partial class UserStatusChecker
    {
        public class UserStatusContainer : BaseUnityContainer
        {
            public class UserStatusModel
            {
                public string Status { get; set; }
            }

            public class ResultAdapter : IResultHandler
            {
                private readonly UserStatusModel _model;

                public ResultAdapter(UserStatusModel model)
                {
                    _model = model;
                }

                public void ErrorHappend(Exception ex)
                {
                    throw ex;
                }

                public void NotFound(string user)
                {
                    _model.Status = "NotFound";
                }

                public void IsOnline(string user)
                {
                    _model.Status = "Online";
                }

                public void IsOffline(string user)
                {
                    _model.Status = "Offline";
                }
            }

            public class UserInfoProvider : IUserInfo
            {
                private readonly string _userName;

                public UserInfoProvider(string userName)
                {
                    _userName = userName;
                }

                public string UserName
                {
                    get { return _userName; }
                }
            }

            public UserStatusContainer(UserStatusModel model, string userName)
            {
                RegisterOnlineChecker(model, userName);
            }

            public IUserStatusChecker GetChecker()
            {
                return this.Resolve<IUserStatusChecker>();
            }
        }
    }
}