﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkingWithMaps.Models;
using System.Net;
using Newtonsoft.Json;
using WorkingWithMaps.Dtos;
using WorkingWithMaps.Extensions;

namespace WorkingWithMaps.Services
{
        public class ClearDriveService : IClearDriveService
        {
            private readonly HttpClient? _httpClient;

            public ClearDriveService(IHttpClientFactory? httpClientFactory)
            {
                if (httpClientFactory is not null)
                {
                    _httpClient = httpClientFactory.CreateClient("ClearDriveApi");
                }
            }

            public async Task<List<Position>> SelectAll()
            {
                if (_httpClient is not null)
                {
                    List<PositionDto>? result = await _httpClient.GetFromJsonAsync<List<PositionDto>>("api/Position");
                    if (result is not null)
                        return result.Select(positionDto => positionDto.ToPosition()).ToList();
                }
                return new List<Position>();
            }

        public async Task<string> DeleteAsync(Guid id)
        {
            string defaultResponse = "";
            if (_httpClient is not null)
            {
                try
                {
                    HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"api/Position/{id}");
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        string? response = JsonConvert.DeserializeObject<string>(content);
                        if (response is null)
                        {
                            defaultResponse="A törlés http kérés hibát okozott!";
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
            defaultResponse="Az adatok törlése nem lehetséges!";
            return defaultResponse;
        }

        public async Task<string> InsertAsync(Position position)
        {
            string defaultResponse = "";
            if (_httpClient is not null)
            {
                HttpResponseMessage? httpResponse = null;
                try
                {
                    httpResponse = await _httpClient.PostAsJsonAsync("api/Position", position.ToPositionDto());
                    if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string content = await httpResponse.Content.ReadAsStringAsync();
                        string? response = JsonConvert.DeserializeObject<string>(content);
                        if (response is null)
                        {
                            defaultResponse = "A mentés http kérés hibát okozott!";
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
            defaultResponse= "Az adatok mentése nem lehetséges!";
            return defaultResponse;
        }
    }
}
