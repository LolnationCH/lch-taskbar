namespace lch_configuration.ComponentOptions
{
  public class NetworkOptions : IComponentOptions
  {
    public bool ShowIcon { get; set; } = true;
    public string TextFormat { get; set; } = "{name} ({strength})";
  }
}
