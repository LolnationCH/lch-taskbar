using NAudio.CoreAudioApi;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for SoundControl.xaml
  /// </summary>
  public partial class SoundControl : System.Windows.Controls.Button
    {
    public SoundControl()
    {
      InitializeComponent();
      Refresh();

      DispatcherTimer LiveTime = new DispatcherTimer();
      LiveTime.Interval = TimeSpan.FromSeconds(1);
      LiveTime.Tick += RefreshSoundControl_tick;
      LiveTime.Start();
    }

    private void RefreshSoundControl_tick(object? sender, EventArgs e)
    {
      Refresh();
    }

    private void VolumeMixer_Click(object sender, RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:apps-volume";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }

    private void Refresh()
    {
      var enumerator = new MMDeviceEnumerator();
      var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
      var volume = device.AudioEndpointVolume.MasterVolumeLevelScalar * 100;
      OutputDeviceLabel.Content = $"{device.FriendlyName} - ({volume:0})%";
    }
  }
}
