using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CAS.mobil.Models;
using System.Net;
using Newtonsoft.Json;
using CAS.mobil.Dtos;
using System.Linq;
using System.Diagnostics;
using CAS.mobil.Responses;
using CAS.mobil.ViewModels;
using Microsoft.Extensions.Logging;
using CAS.mobil.Extensions;

namespace CAS.mobil.Services
{
    public class ClearDriveService : IClearDriveService
    {
        private readonly HttpClient? _httpClient;

        public ClearDriveService(IHttpClientFactory httpClientFactory)
        {
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
            if (_httpClient != null)
            {
                List<PositionDto>? result = await _httpClient.GetFromJsonAsync<List<PositionDto>>("api/Position");
                if (result != null)
                {
                    return result.Select(positionDto => positionDto.ToPosition()).ToList();
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

            HttpResponseMessage? httpResponse = null;
            try
            {
                httpResponse = await _httpClient.PostAsJsonAsync("api/Position", position.ToPositionDto());
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

