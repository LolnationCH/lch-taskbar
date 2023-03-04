using lch_configuration.ComponentOptions;
using System.Windows.Controls;
using System.Windows.Threading;

namespace lch_taskbar.TaskbarComponents
{
  public partial class WeatherControl : System.Windows.Controls.Button
    {
    private readonly WeatherOptions options;
    public WeatherControl(IComponentOptions options)
    {;
      this.options = (options as WeatherOptions)!;
      InitializeComponent();
      SetupWeather();
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
        TimeLabel.Text = WeatherUtils.GetWeather(options.location, options.units);
        ToolTip = options.location;
      });
    }

    public void Refresh()
    {
      SetWeather();
    }

    private void WeatherButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = WeatherUtils.GettWeatherViewingUrl(options.location, options.units);
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
  }
}
