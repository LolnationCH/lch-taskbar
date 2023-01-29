using System.Windows.Controls;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class WeatherControl : System.Windows.Controls.Button
    {
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
        TimeLabel.Content = WeatherUtils.GetWeather();
        ToolTip = Configuration.Configuration.GetInstance().GetData.WeatherLocation;
      });
    }

    public void Refresh()
    {
      SetWeather();
    }

    private void WeatherButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = WeatherUtils.GettWeatherViewingUrl();
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
  }
}
