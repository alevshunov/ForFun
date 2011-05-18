using Example2.Common;

namespace Example2.Bll.UserStatusChecking
{
    public interface IResultHandler : IErrorHandler
    {
        void NotFound(string user);

        void IsOnline(string user);

        void IsOffline(string user);
    }
}