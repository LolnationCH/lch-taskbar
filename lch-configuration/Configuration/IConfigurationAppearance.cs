using Newtonsoft.Json;

namespace lch_taskbar_wpf.Configuration
{
  public enum TaskbarPosition
  {
    Left,
    Right,
    Top,
    Bottom
  }
  
  [JsonObject]
  public class ComponentList
  {
    [JsonProperty]
    public List<string> LeftComponents { get; set; } = new();
    [JsonProperty]
    public List<string> MiddleComponents { get; set; } = new();
    [JsonProperty]
    public List<string> RightComponents { get; set; } = new();

    public List<string> GetLeftComponents()
    {
      if (LeftComponents.Count == 0)
        return new List<string>()
        {
          "processes",
          "weather",
          "spotify",
        };
      return LeftComponents;
    }

    public List<string> GetMiddleComponents()
    {
      if (MiddleComponents.Count == 0)
        return new List<string>()
        {
          "title",
        };
      return MiddleComponents;
    }

    public List<string> GetRightComponents()
    {
      if (RightComponents.Count == 0)
        return new List<string>()
        {
          "volume",
          "bluetooth",
          "everything",
          "network",
          "time"
        };
      return RightComponents;
    }
  }
  
  public interface IConfigurationAppearance
  {
    public string FontSize { get; set; }
    public string FontFamily { get; set; }
    public string FontColor { get; set; }
    public string BackgroundColor { get; set; }
    public string Opacity { get; set; }
    public TaskbarPosition Position { get; set; }
    public ComponentList ComponentList { get; set; }
  }
}
