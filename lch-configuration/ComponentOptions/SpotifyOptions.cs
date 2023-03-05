namespace lch_configuration.ComponentOptions
{
  public class SpotifyOptions : IComponentOptions
  {
    public bool ShowIcon { get; set; } = true;
    public string TextFormat { get; set; } = "{title} - {artist}";

    public int MaxArtistLength { get; set; } = 20;
    public int MaxTitleLength { get; set; } = 20;
  }
}
