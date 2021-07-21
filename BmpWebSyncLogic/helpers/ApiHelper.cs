using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.helpers
{
    public class ApiHelper
    {
        private string userName = "49e8f8cb23692ae9afa9e1d32f265fde31fe45ac";
        private string password = "e0645f81f5803dfffa012634f07585a03e99d0fc";
        private Uri baseUrl = new Uri("https://rurex.ml");
        private Cookie cookie = new Cookie("beta", "t3stuj3my_rUr3x%21");
        HttpClientHandler _handler;
        HttpClient _client;


        public ApiHelper()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseUrl, cookie);
            _handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            _client = new HttpClient(_handler) { BaseAddress = baseUrl };
            _client.Timeout = TimeSpan.FromMinutes(10);
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            _client.DefaultRequestHeaders.ConnectionClose = false;
        }


        internal string Put (string json,string url)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string receiveStream;
            try
            {
                var task = Task.Run(() => _client.PostAsync(url, content));
                task.Wait();
                var response = task.Result;
                receiveStream = response.Content.ReadAsStringAsync().Result;
                receiveStream = receiveStream.Replace("\\/", "/");
            }
            catch (TaskCanceledException ex)
            {
                Logger.LogException($"EX: {ex.Message} {ex.CancellationToken.IsCancellationRequested}");
                receiveStream = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.LogException($"EX: {ex.Message}");
                receiveStream = ex.Message;
            }
            return receiveStream;
        }


        internal async Task<string> Putt(string json, string url)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string receiveStream;
            try
            {
                
                var response = await _client.PostAsync(url, content);
                receiveStream = await response.Content.ReadAsStringAsync();

                
                receiveStream = $"{ JsonHelper.ToJson(response.StatusCode)} response - {receiveStream.Replace("\\/", "/")}";
                    response.Dispose();
                }
            catch (TaskCanceledException ex)
            {
                Logger.LogException($"EX: {ex.Message} {ex.CancellationToken.IsCancellationRequested}");
                Logger.LogException($"EX: {ex}");
                receiveStream = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.LogException($"EX: {ex.Message}");
                receiveStream = ex.Message;
            }
            return receiveStream;
        }


        public string ProductGroupPut(string json)
        {
            string url = "/api/product-group/put";            
            return Put(json, url);            
        }        


        public string ProductGroupDelete(string json)
        {
            string url = "/api/product-group/delete";
            return Put(json, url);
        }


        public async Task<string> ProductPutt(string json)
        {
            string url = "/api/product/put";
            return await Putt(json, url);
        }


        public string ProductPut(string json)
        {
            string url = "/api/product/put";
            return Put(json, url);
        }


        public string ProductDelete(string json)
        {
            string url = "/api/product/delete";
            return Put(json, url);
        }


        public string FileDelete(string json)
        {
            string url = "/api/File/delete";
            return Put(json, url);
        }


        public string FilePut(string json)
        {
            string url = "/api/file/put";
            return Put(json, url);
        }


        public object PricesPut(string json)
        {
            string url = "/api/product-price/put";
            return Put(json, url);
        }


        public object StatusWarehousePut(string json)
        {
            string url = "/api/product-warehouse/put";
            return Put(json, url);
        }


    }      
}
