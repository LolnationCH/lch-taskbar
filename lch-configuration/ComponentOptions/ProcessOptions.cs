using lch_configuration.CustomTypes;

namespace lch_configuration.ComponentOptions
{
  public class ProcessOptions : IComponentOptions
  {
    public string ControlFormat { get; set; } = "{icon}";
    // public string ControlFormat { get; set; } = "{icon} {name}";

    public ProcessList ProcessNamesExcluded { get; set; } = new();
  }
}
