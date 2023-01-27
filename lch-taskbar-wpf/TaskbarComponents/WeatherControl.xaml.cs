﻿using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class WeatherControl : System.Windows.Controls.Label
  {
    const string location = "Montreal";
    
    public WeatherControl()
    {
      InitializeComponent();
      SetupWeather();
      SetWeather();
    }
    private void SetupWeather()
    {
      DispatcherTimer LiveTime = new()
      {
        Interval = TimeSpan.FromHours(2)
      };
      LiveTime.Tick += Weather_Tick;
      LiveTime.Start();
      SetWeather();
    }

    private void Weather_Tick(object? sender, EventArgs e)
    {
      SetWeather();
    }

    private void SetWeather()
    {
      Dispatcher.Invoke(() =>
      {
        Content = WeatherUtils.GetWeather(location);
        ToolTip = location;
      });
    }

    private void WeatherButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "https://wttr.in/" + location;
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
  }
}
