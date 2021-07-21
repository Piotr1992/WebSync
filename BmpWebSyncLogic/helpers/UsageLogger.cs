using System;
using System.Collections.Generic;
using Helpers;
using System.Linq;
using System.Text;
using System.Reflection;
using BmpWebSyncLogic.helpers;


namespace Helpers
{
    public class UsageLogger : IDisposable
    {
        private System.Diagnostics.Stopwatch _execWatch= new System.Diagnostics.Stopwatch();
        private string _appName;
        private string _typeName;
        private string _methodName;
        private string _message;


        public UsageLogger(string appName,string typeName,string methodName,string message)
        {
            if(!LogUsage.Usage) return;
            _execWatch.Start();
            _appName=appName;
            _typeName=typeName;
            _methodName=methodName;
            _message=message;
            Logger.LogDebug($"{_typeName}.{_methodName} {_message}");
        }


        public UsageLogger(string appName,MethodBase methodBase,string message)
        {
            if(!LogUsage.Usage) return;
            _execWatch.Start();
            _appName=appName;
            _typeName=methodBase.ReflectedType.Name;
            _methodName=methodBase.Name;
            _message=message;
            Logger.LogDebug($"{_typeName}.{_methodName} {_message}");
        }


        private void CleanUp(object p)
        {
            if(!LogUsage.Usage) return;
            _execWatch.Stop();
                if (LogUsage.Usage)
                {
                    var logWatch = System.Diagnostics.Stopwatch.StartNew();

                    Log(_appName,_typeName,_methodName,_execWatch.ElapsedMilliseconds,_message);

                    logWatch.Stop();
                    if(logWatch.ElapsedMilliseconds>100) Logger.LogWarn($"{_typeName}.{_methodName} LogUsage LogTime:{logWatch.ElapsedMilliseconds}ms");
                }
        }


        public void Dispose()
        {
            CleanUp(new object());
        }


        private void Log (string appName,string typeName, string methodName, long elapsed, string message)
        {
            List<QueryParam> log = new List<QueryParam>();
            log.Add(new QueryParam("@appName", appName));
            log.Add(new QueryParam("@typeName", typeName));
            log.Add(new QueryParam("@methodName", methodName));
            log.Add(new QueryParam("@elapsed", elapsed));
            log.Add(new QueryParam("@parameters", message));
            DBHelper.RunScalarSqlProcParam("dbo.bmp_spLogUsage",log);
        }

    }
}


                
            


                
