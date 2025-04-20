using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Handle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public class ResourceApi : IResourceApi
    {
        public static readonly HttpClient client = new HttpClient();

        public async Task<T> ApiDetail<T>(string url)
        {
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ResponseHandle._mainForm.AppendImportLog($"Error: {errorContent}");
                    }

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var jsonSerializer = new DataContractJsonSerializer(typeof(T));
                        return (T)jsonSerializer.ReadObject(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex}");
                throw;
            }
        }

        public async Task<List<T>> ApiGet<T>(string url)
        {
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ResponseHandle._mainForm.AppendImportLog($"Error: {errorContent}");
                    }

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));
                        return (List<T>)jsonSerializer.ReadObject(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex}");
                throw;
            }
        }

        public async Task<ApiResponse> ApiPost<T>(string url, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(url, content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ResponseHandle._mainForm.AppendImportLog($"Error: {errorContent}");
                        return null;
                    }

                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponse>(responseData);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex}");
                throw;
            }
        }

        public async Task<List<T>> ApiDelete<T>(string url)
        {
            try
            {
                List<T> deletedData = new List<T>();
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var deletedItem = JsonConvert.DeserializeObject<T>(jsonResponse);
                    deletedData.Add(deletedItem);
                }
                else
                {
                    ResponseHandle._mainForm.AppendImportLog($"Failed to delete data. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                }

                return deletedData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred while deleting data: {ex.Message}");
                throw;
            }
        }
    }
}
