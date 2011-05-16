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


namespace Example2
{
    public partial class Default : Page
    {
        internal class UserMailCheckDataProvider : UserMailChecking.IUserInfo, UserMailChecking.IResultHandler<MailRepository.MailInfo>
        {
            private readonly Default _page;

            public UserMailCheckDataProvider(Default page)
            {
                _page = page;
            }

            public string UserName
            {
                get { return _page.UserNameMailCheckTextBox.Text; }
            }

            public void ShowNoMessagesInfo()
            {
                _page.Mails.Visible = false;
                _page.MailCount.Text = "<b>" + UserName + "</b> has no mail.";
            }

            public void ShowMessages(List<MailRepository.MailInfo> status)
            {
                _page.Mails.Visible = true;
                _page.Mails.DataSource = status;
                _page.Mails.DataBind();
                _page.MailCount.Text = "<b>" + UserName + "</b> has " + status.Count + " mail(s).";
            }
        }

        internal class UserOnlineCheckDataProvider : UserOnlineChecking.IUserInfo, UserOnlineChecking.IResultHandler
        {
            private readonly Default _page;

            public UserOnlineCheckDataProvider(Default page)
            {
                _page = page;
            }

            public string UserName
            {
                get { return _page.UserNameStatusCheckTextBox.Text; }
            }

            public void ShowUserStatusMessage(string message)
            {
                _page.UserStatusMessageLabel.Text = message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private UserStatusChecker UserStatusChecker
        {
            get
            {
                var userRepository = new UserRepository();
                var dataProvider = new UserOnlineCheckDataProvider(this);
                var resultAdapter = new ResultAdapter(dataProvider);
                var checker = new UserStatusChecker(resultAdapter, dataProvider, userRepository);
                return checker;
            }
        }

        private UserMailInfoChecker UserMailInfoChecker
        {
            get
            {
                var repository = new MailRepository();
                var dataProvider = new UserMailCheckDataProvider(this);
                var checker = new UserMailInfoChecker(dataProvider, dataProvider, repository);
                return checker;
            }
        }

        protected void CheckUserStatusButtonClicked(object sender, EventArgs e)
        {
            try
            {
                UserStatusChecker.Process();
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
                UserMailInfoChecker.Process();
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