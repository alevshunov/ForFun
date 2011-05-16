using System;
using System.Linq;
using Example2.Repository;
using Example2.StatusChecker;
using Example2.UserOnlineChecking.ResultAdapter;

namespace Example2.UserOnlineChecking
{
    public class UserStatusChecker : IAction
    {
        private readonly IResultAdapter _resultAdapter;
        private readonly IUserInfo _infoProvider;
        private readonly IUserRepository _repository;

        public UserStatusChecker(
            IResultAdapter resultAdapter,
            IUserInfo infoProvider,
            IUserRepository repository)
        {
            _resultAdapter = resultAdapter;
            _infoProvider = infoProvider;
            _repository = repository;
        }

        protected void Process(bool status)
        {
            try
            {
                if (status)
                    _resultAdapter.IsOnline(_infoProvider.UserName);
                else
                    _resultAdapter.IsOffline(_infoProvider.UserName);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknows exception.", ex);
            }
        }

        protected bool GetStatus(string userName)
        {
            return (from u in _repository.Users
                    where u.UserName == userName
                    select u.IsOnline)
                .First();
        }

        public void Process()
        {
            try
            {
                var status = GetStatus(_infoProvider.UserName);
                Process(status);
            }
            catch(InvalidOperationException)
            {
                _resultAdapter.NotFound(_infoProvider.UserName);
            }
        }
    }
}