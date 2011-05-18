using System;

namespace Example2.Common
{
    public interface IErrorHandler
    {
        void ErrorHappend(Exception ex);
    }
}