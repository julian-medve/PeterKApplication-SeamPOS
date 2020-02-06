using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PeterKApplication.Bases
{
    [DebuggerStepThrough]
    public class HttpClientDiagnosticsHandler : DelegatingHandler
    {
        public HttpClientDiagnosticsHandler(HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            IProgressDialog pd = null;
            var showLoading = true;

            if (request.RequestUri.AbsolutePath.EndsWith("/Sync"))
            {
                showLoading = false;
            }

            if (showLoading)
                try
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            pd = UserDialogs.Instance.Loading("Loading...");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Failed showing loading");
                        }
                    });
                }
                catch (Exception e) {
                    Console.Write("ShowLoading : " + e.ToString());
                }

            try
            {
                var totalElapsedTime = Stopwatch.StartNew();

                Debug.WriteLine("Request: {0}", request);
                if (request.Content != null)
                {
                    var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine("Request Content: {0}", content);
                }

                var auth = request.Headers.Authorization;

                if (auth != null)
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue(auth.Scheme, Preferences.Get("Token", null));
                }

                var responseElapsedTime = Stopwatch.StartNew();
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                Debug.WriteLine("Response: {0}", response);
                if (response.Content != null)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Debug.WriteLine("Response Content: {0}", content);
                }

                responseElapsedTime.Stop();
                Debug.WriteLine("Response elapsed time: {0} ms", responseElapsedTime.ElapsedMilliseconds);

                totalElapsedTime.Stop();
                Debug.WriteLine("Total elapsed time: {0} ms", totalElapsedTime.ElapsedMilliseconds);
                return response;
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        pd?.Hide();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Disposing of progress bar failed:" + e.Message);
                        Thread.Sleep(100);
                        try
                        {
                            pd?.Hide();
                        }
                        catch (Exception e2)
                        {
                            Console.WriteLine("Disposing of progress bar failed second time:" + e2.Message);
                        }
                    }
                });
            }
        }
    }
}