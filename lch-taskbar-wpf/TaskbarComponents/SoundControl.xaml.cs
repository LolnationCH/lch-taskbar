using NAudio.CoreAudioApi;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class SoundControl : System.Windows.Controls.Button, ICustomButton
    {
    public SoundControl()
    {
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    public void Refresh()
    {
      var enumerator = new MMDeviceEnumerator();
      var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
      var volume = device.AudioEndpointVolume.MasterVolumeLevelScalar * 100;
      OutputDeviceLabel.Content = $"{device.FriendlyName} - ({volume:0})%";
    }

    public void CustomButton_Click(object sender, RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:apps-volume";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
  }
}
