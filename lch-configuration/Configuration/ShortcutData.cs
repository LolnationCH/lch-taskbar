using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lch_taskbar_wpf.Configuration
{
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
