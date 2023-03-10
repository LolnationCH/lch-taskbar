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
          ComponentFactory.CreateComponentFromName("processes")!,
          ComponentFactory.CreateComponentFromName("weather")!,
          ComponentFactory.CreateComponentFromName("spotify")!,
          ComponentFactory.CreateComponentFromName("shortcuts")!,
        };
      return LeftComponents;
    }

    public List<Component> GetMiddleComponents()
    {
      if (MiddleComponents.Count == 0)
        return new List<Component>()
        {
          ComponentFactory.CreateComponentFromName("title")!,
        };
      return MiddleComponents;
    }

    public List<Component> GetRightComponents()
    {
      if (RightComponents.Count == 0)
        return new List<Component>()
        {
          ComponentFactory.CreateComponentFromName("volume")!,
          ComponentFactory.CreateComponentFromName("bluetooth")!,
          ComponentFactory.CreateComponentFromName("network")!,
          ComponentFactory.CreateComponentFromName("time")!,
        };
      return RightComponents;
    }
  }
}
