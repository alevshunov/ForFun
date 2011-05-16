using System.Collections.Generic;
using System.Linq;

namespace Example2.Repository
{
    public class UserRepository : IUserRepository
    {
        public class UserInfo
        {
            internal UserInfo(string userName, bool isOnline)
            {
                UserName = userName;
                IsOnline = isOnline;
            }

            public string UserName { get; private set; }

            public bool IsOnline { get; private set; }
        }

        static UserRepository()
        {
            UsersStore = new List<UserInfo>();
            UsersStore.Add(new UserInfo("admin", true));
            UsersStore.Add(new UserInfo("guest", true));
            UsersStore.Add(new UserInfo("hacker", false));
            UsersStore.Add(new UserInfo("alex", false));
            UsersStore.Add(new UserInfo("test", true));
        }

        private static readonly List<UserInfo> UsersStore;

        public IQueryable<UserInfo> Users
        {
            get { return UsersStore.AsQueryable(); }
        }
    }
}