using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;
using ClearDrive.shared.Responses;
using ClearDrive.shared.Models;

namespace ClearDrive.shared.Services
{
    public class ClearDriveService : IClearDriveService
    {
        private readonly HttpClient? _httpClient;

        public ClearDriveService(IHttpClientFactory httpClientFactory)
        {
            ;

            if (httpClientFactory != null)
            {
                _httpClient = httpClientFactory.CreateClient("ClearDriveApi");
            }
            else
            {
                Debug.WriteLine("A HttpClientFactory null.");
            }
        }


        public async Task<List<Position>> SelectAll()
        {

            List<Position>? result = await _httpClient.GetFromJsonAsync<List<Position>>("api/Position");
            if (result != null)
            {
                return result.Select(position => position).ToList();
            }

            return new List<Position>();
        }

        public async Task<ControllerResponse> UpdateAsync(Position position)
        {
            ControllerResponse defaultResponse = new();
            if (_httpClient is not null)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync("api/Position", position);
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        ControllerResponse? response = JsonConvert.DeserializeObject<ControllerResponse>(content);
                        if (response is null)
                        {
                            defaultResponse.ClearAndAddError("A törlés http kérés hibát okozott!");
                        }
                        else return response;
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
                    Console.WriteLine($"{ex.Message}");
                }
            }
            defaultResponse.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return defaultResponse;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse defaultResponse = new();

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


            defaultResponse.ClearAndAddError("Az adatok törlés nem lehetséges!");
            Debug.WriteLine($"{defaultResponse.ToString()}");
            return defaultResponse;
        }

        public async Task<ControllerResponse> InsertAsync(Position position)
        {
            ControllerResponse defaultResponse = new();

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


            defaultResponse.ClearAndAddError("Az adatok mentése nem lehetséges!");
            Debug.WriteLine($"{defaultResponse.ToString()}");
            return defaultResponse;
        }
    }
}
