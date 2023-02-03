using Newtonsoft.Json;

namespace lch_configuration.ComponentOptions
{
  [JsonObject]
  public class ShortcutOptions : IComponentOptions
  {
    [JsonProperty]
    public List<ShortcutData> Data { get; set; } = new();
  }

  [JsonObject]
  public class ShortcutData
  {
    [JsonProperty]
    public string Name { get; set; } = "";
    [JsonProperty]
    public string Path { get; set; } = "";
    [JsonProperty]
    public string Arguments { get; set; } = "";
    [JsonProperty]
    public string? IconPath { get; set; } = null;

  }
}
