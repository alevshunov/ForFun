using System;
using System.Collections.Generic;
using System.Linq;
using Example2.Common;
using Example2.Repository;

namespace Example2.Bll.UserMailChecking
{
    public interface IUserMailInfoChecker 
    {
        void Process();
    }

    public class UserMailInfoChecker : IUserMailInfoChecker
    {
        private readonly IResultHandler<MailRepository.MailInfo> _resultHandler;
        private readonly IUserInfo _infoProvider;
        private readonly IMailRepository _repository;
        private readonly IErrorLogger _errorLogger;

        public UserMailInfoChecker(
            IResultHandler<MailRepository.MailInfo> resultHandler,
            IUserInfo infoProvider, 
            IMailRepository repository,
            IErrorLogger errorLogger)
        {
            _resultHandler = resultHandler;
            _repository = repository;
            _errorLogger = errorLogger;
            _infoProvider = infoProvider;
        }

        protected void Process(List<MailRepository.MailInfo> status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            if (status.Count == 0)
                _resultHandler.ShowNoMessagesInfo();
            else
                _resultHandler.ShowMessages(status);
        }

        protected List<MailRepository.MailInfo> GetStatus(string userName)
        {
            var items = (from m in _repository.Mails
                         where m.Receiver == userName
                         select m).ToList();

            return items;
        }

        public void Process()
        {
            try
            {
                var info = GetStatus(_infoProvider.UserName);
                Process(info);
            }
            catch(Exception ex)
            {
                _errorLogger.Log(ex, this);
                _resultHandler.ErrorHappend(ex);
            }
        }
    }
}