using System;
using System.Collections.Generic;
using Example2.Bll.UserMailChecking;
using Example2.Bll.UserStatusChecking;
using Example2.Common;
using Example2.Repository;
using Microsoft.Practices.Unity;


namespace Example2
{
    public partial class Default
    {
        public class UserMailCheckAdapter : BaseUnityContainer
        {
            public class UserInfoProvider : Bll.UserMailChecking.IUserInfo
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
                private readonly Default _page;
                private readonly Bll.UserMailChecking.IUserInfo _dataProvider;

                public ResultHandler(Default page, Bll.UserMailChecking.IUserInfo dataProvider)
                {
                    _page = page;
                    _dataProvider = dataProvider;
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

                public void ErrorHappend(Exception ex)
                {
                    _page.Mails.Visible = false;
                    _page.MailCount.Text = "Oops. Internal error. Please try again later.";
                }
            }

            public UserMailCheckAdapter(Default page)
            {
                RegisterMailChecker(page);
            }

            public IUserMailInfoChecker GetChecker()
            {
                return this.Resolve<IUserMailInfoChecker>();
            }
        }

        public class UserStatusCheckAdapter : BaseUnityContainer
        {
            public class UserInfoProvider : Bll.UserStatusChecking.IUserInfo
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

            public class ResultHandler : IResultHandler
            {
                private readonly Default _page;

                public ResultHandler(Default page)
                {
                    _page = page;
                }

                private void ShowUserStatusMessage(string message)
                {
                    _page.UserStatusMessageLabel.Text = message;
                }

                public void ErrorHappend(Exception ex)
                {
                    _page.UserStatusMessageLabel.Text = "Oops. Internal error. Please try again later.";
                }

                private void DoSpecificWork()
                {
                    var rnd = new Random();
                    if (rnd.Next(1, 100) < 10)
                        throw new NullReferenceException("Ahha! Joke. :)");
                }

                public void NotFound(string user)
                {
                    DoSpecificWork();

                    ShowUserStatusMessage("User <b>" + user + "</b> not found.");
                }

                public void IsOnline(string user)
                {
                    ShowUserStatusMessage("User <b>" + user + "</b> is online!");
                }

                public void IsOffline(string user)
                {
                    ShowUserStatusMessage("User <b>" + user + "</b> is offline.");
                }
            }

            public UserStatusCheckAdapter(Default page)
            {
                RegisterOnlineChecker(page);
            }

            public IUserStatusChecker GetChecker()
            {
                return this.Resolve<IUserStatusChecker>();
            }
        }
    }
}