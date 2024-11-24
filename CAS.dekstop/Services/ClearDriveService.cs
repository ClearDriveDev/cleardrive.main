using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CAS.dekstop.Models;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using CAS.dekstop.Responses;
using CAS.dekstop.Services;

namespace CAS.desktop.Services
{
    public class ClearDriveService : IClearDriveService
    {
        private readonly HttpClient? _httpClient;
        IHttpClientFactory? _httpClientFactory;

        public ClearDriveService()
        {
            if (_httpClientFactory != null)
            {
                _httpClient = _httpClientFactory.CreateClient("ClearDriveApi");
            }
            else
            {
                Debug.WriteLine("A HttpClientFactory null.");
            }
        }

        public async Task<List<Position>> SelectAll()
        {
            if (_httpClient != null)
            {
                List<Position>? result = await _httpClient.GetFromJsonAsync<List<Position>>("api/Position");
                if (result != null)
                {
                    return result.Select(position => position).ToList();
                }
            }
            return new List<Position>();
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse defaultResponse = new();

            if (_httpClient != null)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"api/Position/{id}");
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response == null)
                        {
                            defaultResponse.ClearAndAddError("A törlés http kérés hibát okozott!");
                        }
                        else
                        {
                            return response;
                        }
                    }
                    else if (!httpResponse.IsSuccessStatusCode)
                    {
                        httpResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        return defaultResponse;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Hiba: {ex.Message}");
                }
            }

            defaultResponse.ClearAndAddError("Az adatok törlés nem lehetséges!");
            Debug.WriteLine($"{defaultResponse.ToString()}");
            return defaultResponse;
        }

        public async Task<ControllerResponse> InsertAsync(Position position)
        {
            ControllerResponse defaultResponse = new();

            if (_httpClient != null)
            {
                HttpResponseMessage? httpResponse = null;
                try
                {
                    httpResponse = await _httpClient.PostAsJsonAsync("api/Position", position);
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response == null)
                        {
                            defaultResponse.ClearAndAddError("A mentés http kérés hibát okozott!");
                        }
                        else
                        {
                            return response;
                        }
                    }
                    else if (!httpResponse.IsSuccessStatusCode)
                    {
                        httpResponse.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        return defaultResponse;
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Hiba: {ex.Message}");
                }
            }

            defaultResponse.ClearAndAddError("Az adatok mentése nem lehetséges!");
            Debug.WriteLine($"{defaultResponse.ToString()}");
            return defaultResponse;
        }
    }
}
