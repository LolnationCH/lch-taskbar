using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using lch_configuration.ComponentOptions;

namespace lch_taskbar.TaskbarComponents
{
  public partial class SpotifyControl : System.Windows.Controls.Button, ICustomButton
  {
    SpotifyOptions options = new();

    public SpotifyControl(IComponentOptions? Options)
    {
      if (Options is SpotifyOptions spotifyOptions)
        options = spotifyOptions;

      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    private void SetSpotifyControl()
    {
      var title = SpotifyUtils.GetSpotifyTitle(options.TextFormat, options.MaxArtistLength, options.MaxTitleLength);
      Dispatcher.Invoke(() =>
      {
        Spotify.Text = title;
        Spotify.ToolTip = title;
        SpotifyIcon.Visibility = options.ShowIcon ? Visibility.Visible : Visibility.Collapsed;
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
