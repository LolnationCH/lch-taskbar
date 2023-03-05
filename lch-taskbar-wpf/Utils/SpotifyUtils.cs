using System.Diagnostics;
using System.IO;

public static class SpotifyUtils
{
  private class SpotifyInformation
  {
    required public string Artist { get; set; }
    required public string Title { get; set; }

    public string ToString(string format, int maxArtistLength, int maxTitleLength)
    {
      var result = format;
      result = result.Replace("{artist}", Artist.Length > maxArtistLength ? Artist.Substring(0, maxArtistLength) + "..." : Artist);
      result = result.Replace("{title}", Title.Length > maxTitleLength ? Title.Substring(0, maxTitleLength) + "..." : Title);
      return result;
    }
  }
  
  readonly static string[] pausedSpotifyTitle = new string[] { "Spotify", "Spotify Free", "Spotify Premium" };

  public static string GetSpotifyTitle(string format, int maxArtistLength, int maxTitleLength)
  {
    var proc = GetSpotifyProcess();
    if (proc == null)
      return "";
    
    if (IsSpotifyPaused(proc.MainWindowTitle))
      return "Paused";

    return GetSpotifyInformation(proc.MainWindowTitle).ToString(format, maxArtistLength, maxTitleLength);
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

  private static SpotifyInformation GetSpotifyInformation(string windowTitle)
  {
    return new SpotifyInformation
    {
      Artist = windowTitle.Split(" - ")[0],
      Title = windowTitle.Split(" - ")[1]
    };
  }
}

