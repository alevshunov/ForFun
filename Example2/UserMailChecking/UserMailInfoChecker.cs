using System;
using System.Collections.Generic;
using System.Linq;
using Example2.Repository;
using Example2.StatusChecker;

namespace Example2.UserMailChecking
{
    public interface IUserMailInfoChecker : IAction
    {
        
    }

    public class UserMailInfoChecker : IUserMailInfoChecker
    {
        private readonly IResultHandler<MailRepository.MailInfo> _statusHandler;
        private readonly IUserInfo _infoProvider;
        private readonly IMailRepository _repository;

        public UserMailInfoChecker(
            IResultHandler<MailRepository.MailInfo> statusHandler,
            IUserInfo infoProvider, 
            IMailRepository repository)
        {
            _statusHandler = statusHandler;
            _repository = repository;
            _infoProvider = infoProvider;
        }

        protected void Process(List<MailRepository.MailInfo> status)
        {
            if (status == null)
                throw new ArgumentNullException("status");

            if (status.Count == 0)
                _statusHandler.ShowNoMessagesInfo();
            else
                _statusHandler.ShowMessages(status);
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
            var info = GetStatus(_infoProvider.UserName);
            Process(info);
        }
    }
}