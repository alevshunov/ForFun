using System;

namespace Example2.UserOnlineChecking.ResultAdapter
{
    public class ResultAdapter : IResultAdapter
    {
        private readonly IResultHandler _handler;

        public ResultAdapter(IResultHandler handler)
        {
            _handler = handler;
        }

        private void DoSpecificWork()
        {
            var rnd = new Random();
            if (rnd.Next(1, 100) < 10)
                throw new NullReferenceException("Ahha! Joke. :)");
        }

        public void NotFound(string user)
        {
            DoSpecificWork();

            _handler.ShowUserStatusMessage("User <b>" + user + "</b> not found.");
        }

        public void IsOnline(string user)
        {
            _handler.ShowUserStatusMessage("User <b>" + user + "</b> is online!");
        }

        public void IsOffline(string user)
        {
            _handler.ShowUserStatusMessage("User <b>" + user + "</b> is offline.");
        }
    }
}