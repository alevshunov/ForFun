using System.Collections.Generic;

namespace Example2.UserMailChecking
{
    public interface IResultHandler<TMessage>
    {
        void ShowNoMessagesInfo();

        void ShowMessages(List<TMessage> status);
    }
}