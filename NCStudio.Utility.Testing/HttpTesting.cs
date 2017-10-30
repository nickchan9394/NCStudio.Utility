using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NCStudio.Utility.Testing
{
    public static class HttpTesting
    {
        public async static Task<ResponseResult> GetAsync(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PostAsync(string uri,string content,Encoding encoding=null,string mediaType="application/json")
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PostAsync(uri,new StringContent(content, encoding??Encoding.UTF8, mediaType));

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PutAsync(string uri, string content, Encoding encoding = null, string mediaType = "application/json")
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PutAsync(uri, new StringContent(content, encoding ?? Encoding.UTF8, mediaType));

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> DeleteAsync(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(uri);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }
    }
}
