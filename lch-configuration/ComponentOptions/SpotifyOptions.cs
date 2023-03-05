namespace lch_configuration.ComponentOptions
{
  public class SpotifyOptions : IComponentOptions
  {
    public bool ShowIcon { get; set; } = true;
    public string TextFormat { get; set; } = "{artist} - {title}";
  }
}
