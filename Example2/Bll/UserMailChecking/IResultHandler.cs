using System.Collections.Generic;
using Example2.Common;

namespace Example2.Bll.UserMailChecking
{
    public interface IResultHandler<TMessage> : IErrorHandler
    {
        void ShowNoMessagesInfo();

        void ShowMessages(List<TMessage> status);
    }
}