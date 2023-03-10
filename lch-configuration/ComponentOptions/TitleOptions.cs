namespace lch_configuration.ComponentOptions
{
  public class TitleOptions : IComponentOptions
  {
    public string TitleFormat { get; set; } = "{title}";
    // public string TitleFormat { get; set; } = "{title} - {process} - {process-id}";
  }
}
