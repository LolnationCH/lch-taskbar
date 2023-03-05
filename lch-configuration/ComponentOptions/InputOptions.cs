namespace lch_configuration.ComponentOptions
{
  public class InputOptions : IComponentOptions
  {
    public string Tooltip { get; set; } = "Input";
    public string? Icon { get; set; }
    public string CommandString { get; set; } = "cmd.exe";
    public string? CommandOptions { get; set; }
    public int MinWidth { get; set; } = 100;
  }
}
