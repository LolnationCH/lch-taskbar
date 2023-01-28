namespace lch_taskbar_wpf.Configuration
{
  public enum TaskbarPosition
  {
    Left,
    Right,
    Top,
    Bottom
  }
  public interface ConfigurationAppearance
  {
    public string FontSize { get; set; }
    public string FontFamily { get; set; }
    public string FontColor { get; set; }
    public string BackgroundColor { get; set; }
    public string Opacity { get; set; }
    public TaskbarPosition Position { get; set; }
    public List<string> ComponentList { get; set; }
  }
}
