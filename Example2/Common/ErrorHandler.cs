using System;

namespace Example2.Common
{
    public class ErrorLogger : IErrorLogger
    {
        public void Log(Exception ex, object source)
        {
            // Save ex.ToString() into file.
        }

        public void Log(Exception ex) 
        {
            Log(ex, null);
        }
    }

    public interface IErrorLogger
    {
        void Log(Exception ex, Object source);

        void Log(Exception ex);
    }
}