using System;
using System.Web.UI;

namespace Example2
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CheckUserStatusButtonClicked(object sender, EventArgs e)
        {
            new UserStatusCheckAdapter(this).GetChecker().Process();
        }
        
        protected void CheckMessagesButtonClicked(object sender, EventArgs e)
        {
            new UserMailCheckAdapter(this).GetChecker().Process();
        }
    }
}