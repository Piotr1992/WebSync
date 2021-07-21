using BmpWebSyncLogic;
using BmpWebSyncLogic.entities;
using BmpWebSyncLogic.helpers;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmpWebSyncTests
{
    public partial class Form1 : Form
    {
        ApiHelper _api = new ApiHelper();
        List<ProductGroup> _groups;
        List<Product> _products;
        List<Files> _files;
        List<PricesProducts> _prices;
        List<StatusWarehouseProducts> _statusWarehouse;

        public Form1()
        {
            InitializeComponent();
        }

        private void log(string msg)
        {
            richTextBox1.Text = richTextBox1.Text + msg + Environment.NewLine;
            Logger.LogDebug(msg);
        }

        private void CheckConnection_Click(object sender, EventArgs e)
        {
            rtb.Text = DBHelper.ConnectionString();
            Logger.LogInfo($"Start");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _groups = ProductGroup.GetGroups();            
            log($"Ilosc grup:{ _groups.Count()}");            
            log($"json:{JsonHelper.ToJson(_groups)}");            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = _api.ProductGroupPut(JsonHelper.ToJson(_groups));            
            log($"response {r}");

           bool trueOrFalse = r.Contains("true");

           if ( trueOrFalse ) ProductGroup.UpdateArchiveGroups(trueOrFalse,_groups);                    

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            _products = Product.GetProducts();
            logWatch.Stop();
            log($"IloscProduktow:{ _products.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");
           // Logger.LogDebug($"{JsonHelper.ToJson(_products)}");

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            logWatch.Restart();
            int chunkSize = 10;
            var chunks = ListExtensions.splitList<Product>(_products, chunkSize);
            logWatch.Stop();
            log($"IloscPaczek:{ chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            int i = 0;
            var logWatchh = System.Diagnostics.Stopwatch.StartNew();
            foreach (var chunk in chunks)
            {
                
                i++;
                Logger.LogDebug($"Processing chunk:{i}");
                logWatch.Restart();
                var r= await _api.ProductPutt(JsonHelper.ToJson(chunk));
                logWatch.Stop();
                log($"Chunk:{i}/{chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");// Json: {JsonHelper.ToJson(chunk)}");
                Logger.LogDebug($"chunk:{i} - {r}");
                Logger.LogDebug($"Waiting");
                int m;
                Random random = new Random();
                m = random.Next(5, 30);
                System.Threading.Thread.Sleep(m*100);
            }
            logWatchh.Stop();
            log($"Paczki:{chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            _files = Files.FillFiles();
            logWatch.Stop();
            log($"IloscPlikow:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");
        }

        private void putfiles_Click(object sender, EventArgs e)
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            logWatch.Restart();
            FtpHelper.uploadFiles(_files);
            logWatch.Stop();
            log($"IloscPaczek:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            List<Files> f = _files.Where(w => w.Uploaded == true).ToList();
            rtb.Text = rtb.Text + Environment.NewLine + JsonHelper.ToJson(f);
            _api.FilePut(JsonHelper.ToJson(f));

        }

        private async void all_Click(object sender, EventArgs e)
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            _files = Files.FillFiles();
            logWatch.Stop();
            log($"IloscPlikow:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            logWatch.Restart();
            FtpHelper.uploadFiles(_files);
            logWatch.Stop();
            log($"IloscPaczek:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            List<Files> f = _files.Where(w => w.Uploaded == true).ToList();
            rtb.Text = rtb.Text + Environment.NewLine + JsonHelper.ToJson(f);
            _api.FilePut(JsonHelper.ToJson(f));

            logWatch.Restart();
            _groups = ProductGroup.GetGroups();
            log($"Ilosc grup:{ _groups.Count()}");

            var r = _api.ProductGroupPut(JsonHelper.ToJson(_groups));
            log($"response {r}");

            _products = Product.GetProducts();
            logWatch.Stop();
            log($"IloscProduktow:{ _products.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            logWatch.Restart();
            int chunkSize = 1000;
            var chunks = ListExtensions.splitList<Product>(_products, chunkSize);
            logWatch.Stop();
            log($"IloscPaczek:{ chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            int i = 0;
            var logWatchh = System.Diagnostics.Stopwatch.StartNew();
            foreach (var chunk in chunks)
            {
                i++;
                Logger.LogDebug($"Processing chunk:{i}");
                logWatch.Restart();
                r = await _api.ProductPutt(JsonHelper.ToJson(chunk));
                logWatch.Stop();
                log($"Chunk:{i}/{chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");          // Json: {JsonHelper.ToJson(chunk)}");
                Logger.LogDebug($"chunk:{i} - {r}");
                Logger.LogDebug($"Waiting");
                int m;
                Random random = new Random();
                m = random.Next(5, 30);
                System.Threading.Thread.Sleep(m * 100);
            }
            logWatchh.Stop();
            log($"Paczki:{chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void getPrices_Click(object sender, EventArgs e)
        {
            _prices = PricesProducts.GetPrices();
            log($"ceny:{ JsonHelper.ToJson( _prices)}");
            log($"Ilosc cen:{ _prices.Count()}");
        }

        private void putPrices_Click(object sender, EventArgs e)
        {
            var r = _api.PricesPut(JsonHelper.ToJson(_prices));
            log($"response {r}");
        }

        private void getStatusWarehouse_Click(object sender, EventArgs e)
        {
            _statusWarehouse = StatusWarehouseProducts.GetStatusWarehouse();
            log($"stany:{ JsonHelper.ToJson(_statusWarehouse)}");
            log($"Ilosc cen:{ _statusWarehouse.Count()}");
        }

        private void putStatusWarehouse_Click(object sender, EventArgs e)
        {
            var r = _api.StatusWarehousePut(JsonHelper.ToJson(_statusWarehouse));
            log($"response {r}");
        }
    }
}
