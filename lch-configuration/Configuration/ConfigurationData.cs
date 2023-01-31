namespace lch_taskbar_wpf.Configuration
{
  public class ConfigurationData : IConfigurationPreference, IConfigurationAppearance
  {
    #region ConfigurationPreference
    public string WeatherLocation { get; set; } = "Montreal";
    public string WeatherUnit { get; set; } = "Metric";
    public string EverythingPath { get; set; } = "C:\\Program Files\\Everything\\Everything.exe";

    #endregion

    #region ConfigurationAppearance
    public string FontSize { get; set; } = "10";
    public string FontFamily { get; set; } = "Arial";
    public string FontColor { get; set; } = "#FFFFFF";
    public string BackgroundColor { get; set; } = "#000000";
    public string Opacity { get; set; } = "0";
    public TaskbarPosition Position { get; set; } = TaskbarPosition.Top;
    public ComponentList ComponentList { get; set; } = new();
    public List<ShortcutData> ShortcutsData { get; set; } = new();
    #endregion
  }
}
