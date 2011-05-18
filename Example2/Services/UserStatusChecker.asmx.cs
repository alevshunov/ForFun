using System.Web.Services;

namespace Example2.Services
{
    /// <summary>
    /// Summary description for UserStatusChecker
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public partial class UserStatusChecker : WebService
    {
        [WebMethod]
        public UserStatusContainer.UserStatusModel UserStatus(string userName)
        {
            var model = new UserStatusContainer.UserStatusModel();
            new UserStatusContainer(model, userName).GetChecker().Process();
            return model;
        }
    }

    
}
