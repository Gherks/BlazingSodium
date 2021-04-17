using BlazingSodium.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlazingSodium.Client.Services
{
    public class EmployeeDataService : EmployeeDataServiceInterface
    {
        private readonly HttpClient _httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEmployee(string name, int age)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Dictionary<string, object> content = new Dictionary<string, object>
            {
                ["Name"] = name,
                ["Age"] = age
            };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("api/employees", httpContent);
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/employees/{id}");

            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/employees/{id}");
            string responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Employee>(responseContent);
        }

        public async Task<Employee> GetEmployee(string name)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/employees/{name}");
            string responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Employee>(responseContent);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/employees");
            string responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(responseContent);
        }
    }
}
