namespace lch_configuration.ComponentOptions
{
  public enum TimeDisplay
  {
    Time,
    Date
  }
  
  public class TimeOptions : IComponentOptions
  {
    public string TimeFormat { get; set; } = "HH:mm:ss";
    public string DateFormat { get; set; } = "dd/MM/yyyy";
    public TimeDisplay Display { get; set; } = TimeDisplay.Time;
  }
}
