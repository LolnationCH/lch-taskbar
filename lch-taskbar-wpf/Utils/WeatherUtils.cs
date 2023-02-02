using lch_configuration.Configuration;
using System.Net.Http;

public static class WeatherUtils
{
  public static string GetWeather()
  {
    (var location, var units) = GetWeatherParameters();

    var url = $"https://wttr.in/{location}?format=3&{units}";
    using var client = new HttpClient();
    var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
    return result;
  }

  public static string GettWeatherViewingUrl()
  {
    (var location, var units) = GetWeatherParameters();
    return $"https://wttr.in/{location}?{units}";
  }

  private static (string,string) GetWeatherParameters()
  {
    string location = Configuration.GetInstance().GetData.WeatherLocation;
    string unit = Configuration.GetInstance().GetData.WeatherUnit.ToLower();
    if (unit == "metric")
      unit = "m";
    else
      unit = "u";

    return (location, unit);
  }
}
