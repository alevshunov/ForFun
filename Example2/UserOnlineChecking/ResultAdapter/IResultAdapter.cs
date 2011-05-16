namespace Example2.UserOnlineChecking.ResultAdapter
{
    public interface IResultAdapter
    {
        void NotFound(string user);

        void IsOnline(string user);

        void IsOffline(string user);
    }
}