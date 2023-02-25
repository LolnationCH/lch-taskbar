using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace lch_taskbar.TaskbarComponents
{
  public partial class SpotifyControl : System.Windows.Controls.Button, ICustomButton
    {
    public SpotifyControl()
    {
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    private void SetSpotifyControl()
    {
      var title = SpotifyUtils.GetSpotifyTitle();
      Dispatcher.Invoke(() =>
      {
        Spotify.Text = title;
        Spotify.ToolTip = title;
      });
    }

    public void Refresh()
    {
      SetSpotifyControl();
    }

    public void CustomButton_Click(object sender, RoutedEventArgs e)
    {
      var SpotifyPath = SpotifyUtils.GetSpotifyPath();
      if (File.Exists(SpotifyPath))
      {
        var process = new Process();
        process.StartInfo.FileName = SpotifyPath;
        process.StartInfo.UseShellExecute = true;
        process.Start();
      }
    }
  }
}
