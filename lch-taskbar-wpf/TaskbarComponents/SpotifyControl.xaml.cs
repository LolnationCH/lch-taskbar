using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class SpotifyControl : System.Windows.Controls.Button
    {
    public SpotifyControl()
    {
      InitializeComponent();      
      SetupSpotifyControl();
    }

    private void SetupSpotifyControl()
    {
      DispatcherTimer LiveTime = new DispatcherTimer();
      LiveTime.Interval = TimeSpan.FromSeconds(1);
      LiveTime.Tick += currentTime_Tick;
      LiveTime.Start();
    }

    private void currentTime_Tick(object? sender, EventArgs e)
    {
      SetSpotifyControl();
    }

    private void SetSpotifyControl()
    {
      var title = SpotifyUtils.GetSpotifyTitle();
      Dispatcher.Invoke(() =>
      {
        Spotify.Content = title;
        Spotify.ToolTip = title;
      });
    }

    private void Spotify_Click(object sender, RoutedEventArgs e)
    {
      var spotifyPath = SpotifyUtils.GetSpotifyPath();
      if (File.Exists(spotifyPath))
      {
        var process = new Process();
        process.StartInfo.FileName = spotifyPath;
        process.StartInfo.UseShellExecute = true;
        process.Start();
      }      
    }
  }
}
