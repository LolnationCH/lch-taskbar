using lch_configuration.ComponentOptions;
using NAudio.CoreAudioApi;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace lch_taskbar.TaskbarComponents
{
  public partial class SoundControl : System.Windows.Controls.Button, ICustomButton
    {
    SoundOptions soundOptions = new();
    public SoundControl(IComponentOptions? options)
    {
      if (options != null)
        soundOptions = (SoundOptions)options;
      
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    private string GetText()
    {
      string text = "";
      MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
      MMDevice device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
      if (soundOptions.ShowMute)
        text += (device.AudioEndpointVolume.Mute ? "🔇" : soundOptions.ShowIcon? "🔈": "") + " ";
      else if (soundOptions.ShowIcon)
        text += "🔊 ";

      if (soundOptions.TextFormat != "")
        text += soundOptions.TextFormat.Replace("{device}", device.FriendlyName).Replace("{volume}", (device.AudioEndpointVolume.MasterVolumeLevelScalar * 100).ToString("0"));
      return text;
    }

    public void Refresh()
    {
      OutputDeviceLabel.Text = GetText();
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
