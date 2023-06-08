using System.Net.Http.Json;
using WeatherApp.Model;

namespace WeatherApp.Services;
public class WeatherService
{
    HttpClient httpClient;

    public WeatherService()
    {
        this.httpClient = new HttpClient();
    }

    public async Task<WeatherData> getWeather(string query)
    {
        WeatherData current = null;
        var response = await httpClient.GetAsync(query);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            current = await response.Content.ReadFromJsonAsync<WeatherData>();
        }
        return current;
    }
}
