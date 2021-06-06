using Newtonsoft.Json;
using NLayerProject.Core.Models;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiServices
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> output = null;

            var response = await _httpClient.GetAsync("categories");
            if (response.IsSuccessStatusCode)
            {
                output = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            }

            return output;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            CategoryDto output = null;

            var response = await _httpClient.GetAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                output = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }

            return output;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto model)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("categories", stringContent);
            if (response.IsSuccessStatusCode)
            {
                model = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return model;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> UpdateAsync(CategoryDto model)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("categories", stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
