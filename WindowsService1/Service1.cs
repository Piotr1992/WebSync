using BmpWebSyncLogic.entities;
using BmpWebSyncLogic.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        ApiHelper _api;
        bool _lop;
        Thread _looperThread;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogDebug($"OnStart");
            _api = new ApiHelper();
            _lop = true;
            (_looperThread = new Thread(new ThreadStart(Looper))).Start();
        }

        protected override void OnStop()
        {
            _lop = false;

        }
        private void Looper()
        {
            Logger.LogDebug($"Looper Started");
            while (_lop)
            {
                SyncGroups();
                Thread.Sleep(60000);
            }
        }

        private void SyncGroups()
        {
            var _groups = ProductGroup.GetGroups();
            Logger.LogDebug($"groups {JsonHelper.ToJson(_groups)}");
            var r = _api.ProductGroupPut(JsonHelper.ToJson(_groups));
            Logger.LogDebug($"response {r}");
        }
    }
}
