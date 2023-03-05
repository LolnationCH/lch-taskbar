namespace lch_configuration.Configuration
{
  public class ConfigurationData : IConfigurationAppearance
  {
    #region ConfigurationAppearance
    public int TaskbarSize { get; set; } = 22;
    public string FontSize { get; set; } = "10";
    public string FontFamily { get; set; } = "Arial";
    public string FontColor { get; set; } = "#FFFFFF";
    public string BackgroundColor { get; set; } = "#000000";
    public string Opacity { get; set; } = "0";
    public TaskbarPosition Position { get; set; } = TaskbarPosition.Top;
    #endregion
    
    public Components ComponentList { get; set; } = new();
  }
}
