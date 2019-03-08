using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sertifi.Models;

namespace Sertifi
{
    public class APIClient : IDisposable
    {
        private readonly TimeSpan timeout;
        private HttpClient httpClient;
        private HttpClientHandler httpClientHandler;
        private readonly string baseUrl;
        private const string ClientUserAgent = "client";
        private const string MediaTypeJson = "application/json";

        public APIClient(string baseUrl, TimeSpan? timeout = null)
        {
            this.baseUrl = NormalizeBaseUrl(baseUrl);
            this.timeout = timeout ?? TimeSpan.FromSeconds(90);
        }

        public async Task<List<Student>> GetAsync(string url)
        {
            try
            {
                EnsureHttpClientCreated();

                using (var response = await httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    return await Task.Run(() => JsonConvert.DeserializeObject<List<Student>>(content));
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

        public async Task<string> PutAsync(string url, object input)
        {
            return await PutAsync(url, new StringContent(JsonConvert.SerializeObject(input, Formatting.Indented), Encoding.UTF8, MediaTypeJson));
        }

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            EnsureHttpClientCreated();

            using (var response = await httpClient.PutAsync(url, content))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public void Dispose()
        {
            httpClientHandler?.Dispose();
            httpClient?.Dispose();
        }

        public void CreateHttpClient()
        {
            httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };

            httpClient = new HttpClient(httpClientHandler, false)
            {
                Timeout = timeout
            };

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(ClientUserAgent);

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                httpClient.BaseAddress = new Uri(baseUrl);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
        }

        public void EnsureHttpClientCreated()
        {
            if (httpClient == null)
            {
                CreateHttpClient();
            }
        }

        public static string NormalizeBaseUrl(string url)
        {
            return url.EndsWith("/") ? url : url + "/";
        }
    }
}
