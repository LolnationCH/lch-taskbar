﻿namespace lch_taskbar_wpf.Configuration
{
  public interface IConfigurationPreference
  {
    public string WeatherLocation { get; set; }
    public string WeatherUnit { get; set; }
    public string EverythingPath { get; set; }
  }
}