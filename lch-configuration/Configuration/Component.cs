using lch_configuration.ComponentOptions;
using Newtonsoft.Json;

namespace lch_configuration.Configuration
{
  [JsonObject]
  public class Component
  {
    [JsonProperty]
    public string? Name { get; set; }
    
    [JsonProperty]
    public IComponentOptions? Options { get; set; }
  }
}
