﻿namespace lch_taskbar_wpf.Configuration
{
  public class ConfigurationData : ConfigurationPreference, ConfigurationAppearance
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
    public List<string> ComponentList { get; set; } = new();
    #endregion
  }
}
