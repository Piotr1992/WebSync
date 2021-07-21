using BmpWebSyncLogic.entities;
using BmpWebSyncLogic.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSync
{
    class Program
    {
        static void Main(string[] args)
        {
            doo();
        }

        private static void log(string msg)
        {
            Console.WriteLine( msg);
            Logger.LogDebug(msg);
        }

        private static async void doo(){
            ApiHelper _api = new ApiHelper();
            List<ProductGroup> _groups;
            List<Product> _products;
            List<Files> _files;

            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            _files = Files.FillFiles();
            logWatch.Stop();
            log($"IloscPlikow:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            logWatch.Restart();
            FtpHelper.uploadFiles(_files);
            logWatch.Stop();
            log($"IloscPaczek:{ _files.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

            List<Files> f = _files.Where(w => w.Uploaded == true).ToList();
            _api.FilePut(JsonHelper.ToJson(f));

            logWatch.Restart();
            _groups = ProductGroup.GetGroups();
            log($"Ilosc grup:{ _groups.Count()}");

            var r = _api.ProductGroupPut(JsonHelper.ToJson(_groups));
            log($"response {r}");

            log($"getting products");
            _products = Product.GetProducts();
            logWatch.Stop();
            log($"IloscProduktow:{ _products.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");

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
                var task = _api.ProductPutt(JsonHelper.ToJson(chunk));
                task.Wait();
                r = task.Result;
                logWatch.Stop();
                log($"Chunk:{i}/{chunks.Count()} Elapsed:{logWatch.ElapsedMilliseconds}");
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
    }
}
