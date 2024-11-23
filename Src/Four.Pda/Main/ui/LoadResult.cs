using Com.Crashlytics.Android;
using Four.Pda.Client.Exceptions;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by asavinova on 24/01/16.
    /// </summary>
    public class LoadResult<T>
    {
        private T data;
        private Exception exception;
        public LoadResult(T data)
        {
            this.data = data;
        }

        public LoadResult(Exception exception)
        {
            this.exception = exception;
            if (exception is ParseException)
            {
                Crashlytics.LogException(exception);
            }
        }

        public virtual bool IsSuccess()
        {
            return exception == null;
        }

        public virtual bool IsError()
        {
            return exception != null;
        }

        public virtual T GetData()
        {
            return data;
        }

        public virtual Exception GetException()
        {
            return exception;
        }
    }
}