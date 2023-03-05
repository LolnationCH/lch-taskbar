namespace lch_configuration.Configuration
{
  public enum TaskbarPosition
  {
    Left,
    Right,
    Top,
    Bottom
  }
  
  public interface IConfigurationAppearance
  {
    public int TaskbarSize { get; set; }
    public string FontSize { get; set; }
    public string FontFamily { get; set; }
    public string FontColor { get; set; }
    public string BackgroundColor { get; set; }
    public string Opacity { get; set; }
    public TaskbarPosition Position { get; set; }
}
}
