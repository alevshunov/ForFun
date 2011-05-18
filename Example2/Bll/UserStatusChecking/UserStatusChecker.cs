using System;
using System.Linq;
using Example2.Common;
using Example2.Repository;

namespace Example2.Bll.UserStatusChecking
{
    public interface IUserStatusChecker
    {
        void Process();
    }

    public class UserStatusChecker : IUserStatusChecker
    {
        private readonly IResultHandler _resultHandler;
        private readonly IUserInfo _infoProvider;
        private readonly IUserRepository _repository;
        private readonly IErrorLogger _errorLogger;

        public UserStatusChecker(
            IResultHandler resultHandler,
            IUserInfo infoProvider,
            IUserRepository repository,
            IErrorLogger errorLogger)
        {
            _resultHandler = resultHandler;
            _infoProvider = infoProvider;
            _repository = repository;
            _errorLogger = errorLogger;
        }

        protected void Process(bool status)
        {
            try
            {
                if (status)
                    _resultHandler.IsOnline(_infoProvider.UserName);
                else
                    _resultHandler.IsOffline(_infoProvider.UserName);
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
                try
                {
                    var status = GetStatus(_infoProvider.UserName);
                    Process(status);
                }
                catch (InvalidOperationException)
                {
                    _resultHandler.NotFound(_infoProvider.UserName);
                }
            }
            catch (Exception ex)
            {
                _errorLogger.Log(ex, this);
                _resultHandler.ErrorHappend(ex);
            }
        }
    }
}