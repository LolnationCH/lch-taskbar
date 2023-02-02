namespace lch_configuration.Configuration
{
  public interface IConfigurationPreference
  {
    public string WeatherLocation { get; set; }
    public string WeatherUnit { get; set; }
    public string EverythingPath { get; set; }
  }
}
