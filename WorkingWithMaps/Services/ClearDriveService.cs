using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkingWithMaps.Models;
using System.Net;
using Newtonsoft.Json;
using WorkingWithMaps.Dtos;
using System.Linq;
using System.Diagnostics;
using WorkingWithMaps.Responses;
using WorkingWithMaps.ViewModels;
using Microsoft.Extensions.Logging;
using WorkingWithMaps.Extensions;

namespace WorkingWithMaps.Services
{
    public class ClearDriveService : IClearDriveService
    {
        private readonly HttpClient? _httpClient;

        // Konstruktor, ami biztosítja az IHttpClientFactory injektálását
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

        // A SelectAll metódus, amely lekéri a pozíciókat az API-ból
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

        // A DeleteAsync metódus, amely töröl egy pozíciót
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

