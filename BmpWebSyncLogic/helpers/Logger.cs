using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace BmpWebSyncLogic.helpers
{
    public class Logger
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Logger));
        public static readonly bool IsDebugEnabled = log.IsDebugEnabled;

        private static object locker = new object();


        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            IsDebugEnabled = log.IsDebugEnabled;
        }


        public static void LogException(Exception ex)
        {
            log.ErrorFormat("Exception: " + ex.ToString() + (ex.StackTrace ?? String.Empty));
        }


        public static void LogException(string ex)
        {
            log.ErrorFormat("Exception: " + ex);
        }


        public static void LogInfo(string message)
        {

            log.Info(message);
        }


        public static void LogDebug(string message)
        {
            log.Debug(message);
        }


        public static void LogWarn(string message)
        {
            log.Warn(message);
        }
        
    }
}
