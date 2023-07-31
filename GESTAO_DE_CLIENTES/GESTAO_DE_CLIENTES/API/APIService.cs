using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GESTAO_DE_CLIENTES.API
{
    public class APIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        public APIService()
        {
            _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_apiBaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public string ConvertToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public async Task<DataTable> Get(string endPoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(content);

                    return dataTable;
                }
                else
                {
                    var errorObject = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao chamar a API: {ex.Message}");
            }
        }
        public async Task<DataTable> Get(string endPoint, int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endPoint + "/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(content);

                    return dataTable;
                }
                else
                {
                    var errorObject = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao chamar a API: {ex.Message}");
            }
        }
        public async Task Post(string endPoint, string jsonBody)
        {
            try
            {
                HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endPoint, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorObject = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao chamar a API: {ex.Message}");
            }
        }
        public async Task Put(string endPoint, string jsonBody)
        {
            try
            {
                HttpContent httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(endPoint, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorObject = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao chamar a API: {ex.Message}");
            }
        }
        public async Task Delete(string endPoint, int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(endPoint + "/" + id.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    var errorObject = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao chamar a API: {ex.Message}");
            }
        }


    }
}