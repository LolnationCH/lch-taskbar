using System.Net.Http;

public static class WeatherUtils
{
  public static string GetWeather(string location)
  {
    var url = $"https://wttr.in/{location}?format=3";
    using var client = new HttpClient();
    var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
    return result;
  }
}
