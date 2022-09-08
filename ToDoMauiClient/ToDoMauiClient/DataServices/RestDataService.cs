using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices
{
  public class RestDataService : IRestDataService
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public RestDataService(HttpClient httpClient)
    {
      _httpClient = httpClient;
      _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5254" : "https://localhost:7254";
      _url = $"{_baseAddress}/api";
      _jsonSerializerOptions = new JsonSerializerOptions
      { 
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };
    }

    public async Task AddToDoAsync(ToDo data)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("----<> No Internet Access");
        return;
      }

      try
      {
        string jsonToDo = JsonSerializer.Serialize<ToDo>(data, _jsonSerializerOptions);
        StringContent context = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", context);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("----<> Successfully created ToDo");
        }
        else
        {
          Debug.WriteLine("----<> Failed to created ToDo");
        }

      }
      catch (Exception ex)
      {
        Debug.WriteLine("------<>  Sorry error message - " + ex.Message);
      }

      return;
    }

    public async Task DeleteToDoAsync(int id)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("------<> No Internet Connection Detected");
        return;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("------<> Successfully deleted ToDo object");
        }
        else
        {
          Debug.WriteLine("-------<> Failed to delete ToDo object");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Error occured on main DeleteToDoAsync operation - " + ex.Message);
      }

      return;
    }

    public async Task<List<ToDo>> GetAllToDosAsync()
    {
      List<ToDo> todos = new List<ToDo>();

      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("-----<> No Internet Access");
        return todos;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");

        if (response.IsSuccessStatusCode)
        {
          string context = await response.Content.ReadAsStringAsync();
          todos = JsonSerializer.Deserialize<List<ToDo>>(context, _jsonSerializerOptions);
        }
        else
        {
          Debug.WriteLine("-----<> Non http 2xx response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Whoops exec " + ex.Message);
      }

      return todos;
    }

    public async Task UpdateToDoAsync(ToDo data)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("------<> No Internet Connection Detected");
        return;
      }

      try
      {
        string jsonToDo = JsonSerializer.Serialize<ToDo>(data, _jsonSerializerOptions);
        StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{data.Id}", content);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine($"------<> Successfully updated ToDo Object");
        }
        else
        {
          Debug.WriteLine($"------<> Failed to update ToDo Object \n{jsonToDo}");
        }

      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error occur in main UpdateToDoAsync operation - {ex.Message}");
      }

      return;
    }
  
  
  
  }
}
