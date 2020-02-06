using System;
using System.Net.Http;
using PeterKApplication.Bases;
using PeterKApplication.Refit;
using Refit;

namespace PeterKApplication.Services
{
    public class PublicApiService
    {
        private IPublicApi _client;
        public IPublicApi Client => _client;
        
        public PublicApiService()
        {
            Rebuild();
        }

        private void Rebuild()
        {
            var httpClient = new HttpClient(new HttpClientDiagnosticsHandler())
            {
                BaseAddress = new Uri("https://seampos.azurewebsites.net/api")
            };

            _client = RestService.For<IPublicApi>(httpClient);
        }
    }
}