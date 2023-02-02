namespace lch_configuration.ComponentOptions
{
  public class WeatherOptions : IComponentOptions
  {
    public string location { get; set; } = "Montreal";
    public string units { get; set; } = "metric";
  }
}
