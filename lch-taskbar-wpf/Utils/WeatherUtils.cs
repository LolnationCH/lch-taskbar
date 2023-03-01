using System.Net.Http;

public static class WeatherUtils
{
  public static string GetWeather(string location, string units)
  {
    units = ParseUnits(units);
    var url = $"https://wttr.in/{location}?format=\"%l:+%t\\n\"&{units}";
    using var client = new HttpClient();

    var response = client.GetAsync(url).Result;
    if (response.IsSuccessStatusCode)
    {
      var content = response.Content.ReadAsStringAsync().Result.Replace("\"","");
      return content;
    }
    return $"Error getting weather for {location}";
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
