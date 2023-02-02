using System.Net.Http;

public static class WeatherUtils
{
  public static string GetWeather(string location, string units)
  {
    units = ParseUnits(units);
    var url = $"https://wttr.in/{location}?format=3&{units}";
    using var client = new HttpClient();
    var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
    return result;
  }

  public static string GettWeatherViewingUrl(string location, string units)
  {
    units = ParseUnits(units);
    return $"https://wttr.in/{location}?{units}";
  }

  private static string ParseUnits(string unit)
  {
    if (unit == "metric")
      unit = "m";
    else
      unit = "u";

    return unit;
  }
}
