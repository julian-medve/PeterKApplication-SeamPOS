using System;
using System.Net.Http;
using PeterKApplication.Bases;
using PeterKApplication.Refit;
using Refit;

namespace PeterKApplication.Services
{
    public class PrivateApiService
    {
        
        private IPrivateApi _client;
        public IPrivateApi Client => _client;
        
        public PrivateApiService()
        {
            Rebuild();
        }

        private void Rebuild()
        {
            var httpClient = new HttpClient(new HttpClientDiagnosticsHandler())
            {
                BaseAddress = new Uri("https://seampos.azurewebsites.net/api")
            };

            _client = RestService.For<IPrivateApi>(httpClient);
        }
    }
}