using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Example2.Repository;
using Example2.StatusChecker;
using Example2.UserMailChecking;
using Example2.UserOnlineChecking;
using Example2.UserOnlineChecking.ResultAdapter;
using Microsoft.Practices.Unity;

namespace Example2
{
    public partial class Default : Page
    {
        public class UserMailCheckAdapter : UnityContainer
        {
            public class UserInfoProvider : UserMailChecking.IUserInfo
            {
                private readonly Default _page;

                public UserInfoProvider(Default page)
                {
                    _page = page;
                }
                
                public string UserName
                {
                    get { return _page.UserNameMailCheckTextBox.Text; }
                }
            }

            public class ResultHandler : IResultHandler<MailRepository.MailInfo>
            {
                private Default _page;
                private UserMailChecking.IUserInfo _dataProvider;

                //public ResultHandler(Default page, UserMailChecking.IUserInfo dataProvider)
                //{
                //    _page = page;
                //    _dataProvider = dataProvider;
                //}

                public Default Page
                {
                    get { return _page; }
                    set { _page = value; }
                }

                public UserMailChecking.IUserInfo DataProvider
                {
                    get { return _dataProvider; }
                    set { _dataProvider = value; }
                }

                public void ShowNoMessagesInfo()
                {
                    _page.Mails.Visible = false;
                    _page.MailCount.Text = "<b>" + _dataProvider.UserName + "</b> has no mail.";
                }

                public void ShowMessages(List<MailRepository.MailInfo> status)
                {
                    _page.Mails.Visible = true;
                    _page.Mails.DataSource = status;
                    _page.Mails.DataBind();
                    _page.MailCount.Text = "<b>" + _dataProvider.UserName + "</b> has " + status.Count + " mail(s).";
                }
            }

            public UserMailCheckAdapter(Default page)
            {
                this.RegisterType<IMailRepository, MailRepository>();
                this.RegisterType<UserMailChecking.IUserInfo, UserInfoProvider>(new InjectionConstructor(page));
                this.RegisterType<IResultHandler<MailRepository.MailInfo>, ResultHandler>(new InjectionProperty("Page", page), new InjectionProperty("DataProvider"));
                this.RegisterType<IUserMailInfoChecker, UserMailInfoChecker>();
            }

            public IUserMailInfoChecker GetChecker()
            {
                return this.Resolve<IUserMailInfoChecker>();
            }
        }
        
        public class UserOnlineCheckAdapter : UnityContainer
        {
            public class UserInfoProvider : UserOnlineChecking.IUserInfo
            {
                private readonly Default _page;

                public UserInfoProvider(Default page)
                {
                    _page = page;
                }

                public string UserName
                {
                    get { return _page.UserNameStatusCheckTextBox.Text; }
                }
            }

            public class ResultHandler : UserOnlineChecking.IResultHandler
            {
                private readonly Default _page;

                public ResultHandler(Default page)
                {
                    _page = page;
                }

                public void ShowUserStatusMessage(string message)
                {
                    _page.UserStatusMessageLabel.Text = message;
                }
            }

            public UserOnlineCheckAdapter(Default page)
            {
                this.RegisterType<IUserRepository, UserRepository>();
                this.RegisterType<UserOnlineChecking.IUserInfo, UserInfoProvider>(new InjectionConstructor(page));
                this.RegisterType<IResultHandler, ResultHandler>(new InjectionConstructor(page));
                this.RegisterType<IResultAdapter, ResultAdapter>();
                this.RegisterType<IUserStatusChecker, UserStatusChecker>();
            }

            public IUserStatusChecker GetChecker()
            {
                return this.Resolve<IUserStatusChecker>();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CheckUserStatusButtonClicked(object sender, EventArgs e)
        {
            try
            {
                new UserOnlineCheckAdapter(this).GetChecker().Process();
            }
            catch(Exception ex)
            {
                // Save full error info to error log: ex.ToString()
                UserStatusMessageLabel.Text = "Internal error. Try again later.";
            }
        }
        
        protected void CheckMessagesButtonClicked(object sender, EventArgs e)
        {
            try
            {
                new UserMailCheckAdapter(this).GetChecker().Process();
            }
            catch (Exception ex)
            {
                // Save full error info to error log: ex.ToString()
                Mails.Visible = false;
                MailCount.Text = "Internal error. Try again later.";
            } 
        }
    }
}