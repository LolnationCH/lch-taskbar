using lch_configuration.ComponentOptions;
using Newtonsoft.Json;

namespace lch_configuration.Configuration
{
  [JsonObject]
  public class Components
  {
    [JsonProperty]
    public List<Component> LeftComponents { get; set; } = new();
    [JsonProperty]
    public List<Component> MiddleComponents { get; set; } = new();
    [JsonProperty]
    public List<Component> RightComponents { get; set; } = new();

    public void SetAsDefault()
    {
      LeftComponents = GetLeftComponents();
      MiddleComponents = GetMiddleComponents();
      RightComponents = GetRightComponents();
    }

    public List<Component> GetLeftComponents()
    {
      if (LeftComponents.Count == 0)
        return new List<Component>()
        {
          new Component() {Name = "processes" },
          new Component() {Name = "weather" },
          new Component() {Name = "spotify" },
          new Component() {Name = "shortcut", Options = new ShortcutDatas(){
            Data = new()
            {
              {
                new ShortcutData()
              {
               Name = "Discord",
              Path = "%appdata%\\Discord\\Update.exe",
              Arguments = "--processStart Discord.exe",
              IconPath = "%appdata%\\..\\Local\\Discord\\app.ico"
              } }
            }
          } },
        };
      return LeftComponents;
    }

    public List<Component> GetMiddleComponents()
    {
      if (MiddleComponents.Count == 0)
        return new List<Component>()
        {
          new Component() {Name = "title" },
        };
      return MiddleComponents;
    }

    public List<Component> GetRightComponents()
    {
      if (RightComponents.Count == 0)
        return new List<Component>()
        {
          new Component() {Name = "volume" },
          new Component() {Name = "bluetooth" },
          new Component() {Name = "everything" },
          new Component() {Name = "network" },
          new Component() {Name = "time" },
        };
      return RightComponents;
    }
  }
}
