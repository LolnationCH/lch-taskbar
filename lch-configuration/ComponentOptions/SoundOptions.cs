namespace lch_configuration.ComponentOptions
{
  public class SoundOptions : IComponentOptions
  {
    public bool ShowIcon { get; set; } = false;
    public bool ShowMute { get; set; } = true;
    public string TextFormat { get; set; } = "{device} - ({volume})%"; 
  }
}
