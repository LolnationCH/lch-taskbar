using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SpotifyUtils
{
  readonly static string[] pausedSpotifyTitle = new string[] { "Spotify", "Spotify Free", "Spotify Premium" };

  public static string GetSpotifyTitle()
  {
    var proc = GetSpotifyProcess();
    if (proc == null)
      return "";
    
    if (IsSpotifyPaused(proc.MainWindowTitle))
      return "Paused";

    return proc.MainWindowTitle;
  }

  private static bool IsSpotifyPaused(string title)
  {
    return pausedSpotifyTitle.Contains(title);
  }

  public static Process? GetSpotifyProcess()
  {
    return Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
  }

  public static string GetSpotifyPath()
  {
    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Spotify\\Spotify.exe");
  }
}

