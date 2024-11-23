using Android.Text;
using Android.Util;
using Com.Crashlytics.Android;
using Org.Slf4j;
using Org.Slf4j.Helpers;
using System.Diagnostics;

namespace Four.Pda.Logs
{
    /// <summary>
    /// Created by asavinova on 21/10/15.
    /// </summary>
    public class PdaLogger : Logger
    {
        private static readonly string TAG = "4PDA";
        private int level = Log.VERBOSE;
        private readonly string name;
        public PdaLogger(string name)
        {
            this.name = name;
        }

        public virtual void SetLevel(int level)
        {
            this.level = level;
        }

        public virtual string GetName()
        {
            return name;
        }

        public virtual bool IsTraceEnabled()
        {
            return level <= Log.VERBOSE;
        }

        public virtual void Trace(string msg)
        {
            Log(Log.VERBOSE, msg);
        }

        public virtual void Trace(string format, object arg)
        {
            Log(Log.VERBOSE, format, arg);
        }

        public virtual void Trace(string format, object arg1, object arg2)
        {
            Log(Log.VERBOSE, format, arg1, arg2);
        }

        public virtual void Trace(string format, params object[] arguments)
        {
            Log(Log.VERBOSE, format, arguments);
        }

        public virtual void Trace(string msg, Throwable t)
        {
            Log(Log.VERBOSE, msg, t);
        }

        public virtual bool IsTraceEnabled(Marker marker)
        {
            return IsTraceEnabled();
        }

        public virtual void Trace(Marker marker, string msg)
        {
            Log(Log.VERBOSE, msg);
        }

        public virtual void Trace(Marker marker, string format, object arg)
        {
            Log(Log.VERBOSE, format, arg);
        }

        public virtual void Trace(Marker marker, string format, object arg1, object arg2)
        {
            Log(Log.VERBOSE, format, arg1, arg2);
        }

        public virtual void Trace(Marker marker, string format, params object[] arguments)
        {
            Log(Log.VERBOSE, format, arguments);
        }

        public virtual void Trace(Marker marker, string msg, Throwable t)
        {
            Log(Log.VERBOSE, msg, t);
        }

        public virtual bool IsDebugEnabled()
        {
            return level <= Log.DEBUG;
        }

        public virtual void Debug(string msg)
        {
            Log(Log.DEBUG, msg);
        }

        public virtual void Debug(string format, object arg)
        {
            Log(Log.DEBUG, format, arg);
        }

        public virtual void Debug(string format, object arg1, object arg2)
        {
            Log(Log.DEBUG, format, arg1, arg2);
        }

        public virtual void Debug(string format, params object[] arguments)
        {
            Log(Log.DEBUG, format, arguments);
        }

        public virtual void Debug(string msg, Throwable t)
        {
            Log(Log.DEBUG, msg, t);
        }

        public virtual bool IsDebugEnabled(Marker marker)
        {
            return IsDebugEnabled();
        }

        public virtual void Debug(Marker marker, string msg)
        {
            Log(Log.DEBUG, msg);
        }

        public virtual void Debug(Marker marker, string format, object arg)
        {
            Log(Log.DEBUG, format, arg);
        }

        public virtual void Debug(Marker marker, string format, object arg1, object arg2)
        {
            Log(Log.DEBUG, format, arg1, arg2);
        }

        public virtual void Debug(Marker marker, string format, params object[] arguments)
        {
            Log(Log.DEBUG, format, arguments);
        }

        public virtual void Debug(Marker marker, string msg, Throwable t)
        {
            Log(Log.DEBUG, msg, t);
        }

        public virtual bool IsInfoEnabled()
        {
            return level <= Log.INFO;
        }

        public virtual void Info(string msg)
        {
            Log(Log.INFO, msg);
        }

        public virtual void Info(string format, object arg)
        {
            Log(Log.INFO, format, arg);
        }

        public virtual void Info(string format, object arg1, object arg2)
        {
            Log(Log.INFO, format, arg1, arg2);
        }

        public virtual void Info(string format, params object[] arguments)
        {
            Log(Log.INFO, format, arguments);
        }

        public virtual void Info(string msg, Throwable t)
        {
            Log(Log.INFO, msg, t);
        }

        public virtual bool IsInfoEnabled(Marker marker)
        {
            return IsInfoEnabled();
        }

        public virtual void Info(Marker marker, string msg)
        {
            Log(Log.INFO, msg);
        }

        public virtual void Info(Marker marker, string format, object arg)
        {
            Log(Log.INFO, format, arg);
        }

        public virtual void Info(Marker marker, string format, object arg1, object arg2)
        {
            Log(Log.INFO, format, arg1, arg2);
        }

        public virtual void Info(Marker marker, string format, params object[] arguments)
        {
            Log(Log.INFO, format, arguments);
        }

        public virtual void Info(Marker marker, string msg, Throwable t)
        {
            Log(Log.INFO, msg, t);
        }

        public virtual bool IsWarnEnabled()
        {
            return level <= Log.WARN;
        }

        public virtual void Warn(string msg)
        {
            Log(Log.WARN, msg);
        }

        public virtual void Warn(string format, object arg)
        {
            Log(Log.WARN, format, arg);
        }

        public virtual void Warn(string format, object arg1, object arg2)
        {
            Log(Log.WARN, format, arg1, arg2);
        }

        public virtual void Warn(string format, params object[] arguments)
        {
            Log(Log.WARN, format, arguments);
        }

        public virtual void Warn(string msg, Throwable t)
        {
            Log(Log.WARN, msg, t);
        }

        public virtual bool IsWarnEnabled(Marker marker)
        {
            return IsWarnEnabled();
        }

        public virtual void Warn(Marker marker, string msg)
        {
            Log(Log.WARN, msg);
        }

        public virtual void Warn(Marker marker, string format, object arg)
        {
            Log(Log.WARN, format, arg);
        }

        public virtual void Warn(Marker marker, string format, object arg1, object arg2)
        {
            Log(Log.WARN, format, arg1, arg2);
        }

        public virtual void Warn(Marker marker, string format, params object[] arguments)
        {
            Log(Log.WARN, format, arguments);
        }

        public virtual void Warn(Marker marker, string msg, Throwable t)
        {
            Log(Log.WARN, msg, t);
        }

        public virtual bool IsErrorEnabled()
        {
            return level <= Log.ERROR;
        }

        public virtual void Error(string msg)
        {
            Log(Log.ERROR, msg);
        }

        public virtual void Error(string format, object arg)
        {
            Log(Log.ERROR, format, arg);
        }

        public virtual void Error(string format, object arg1, object arg2)
        {
            Log(Log.ERROR, format, arg1, arg2);
        }

        public virtual void Error(string format, params object[] arguments)
        {
            Log(Log.ERROR, format, arguments);
        }

        public virtual void Error(string msg, Throwable t)
        {
            Log(Log.ERROR, msg, t);
        }

        public virtual bool IsErrorEnabled(Marker marker)
        {
            return IsErrorEnabled();
        }

        public virtual void Error(Marker marker, string msg)
        {
            Log(Log.ERROR, msg);
        }

        public virtual void Error(Marker marker, string format, object arg)
        {
            Log(Log.ERROR, format, arg);
        }

        public virtual void Error(Marker marker, string format, object arg1, object arg2)
        {
            Log(Log.ERROR, format, arg1, arg2);
        }

        public virtual void Error(Marker marker, string format, params object[] arguments)
        {
            Log(Log.ERROR, format, arguments);
        }

        public virtual void Error(Marker marker, string msg, Throwable t)
        {
            Log(Log.ERROR, msg, t);
        }

        private void Log(int priority, string message)
        {
            Log(priority, message, (Throwable)null);
        }

        private void Log(int priority, string format, params object[] args)
        {
            string message = MessageFormatter.ArrayFormat(format, args).GetMessage();
            Log(priority, message, (Throwable)null);
        }

        private void Log(int priority, string message, Throwable t)
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.Append(name).Append(':').Append(' ');
            if (!TextUtils.IsEmpty(message))
            {
                messageBuilder.Append(message);
            }

            if (t != null)
            {
                string stackTraceString = Log.GetStackTraceString(t);
                messageBuilder.Append('\n').Append(stackTraceString);
            }

            string msg = messageBuilder.ToString();
            Log.Println(priority, TAG, msg);
            Crashlytics.Log(priority, TAG, msg);
        }
    }
}