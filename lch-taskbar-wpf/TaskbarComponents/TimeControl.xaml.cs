using lch_configuration.ComponentOptions;
using System.Windows;

namespace lch_taskbar.TaskbarComponents
{
  public partial class TimeControl : System.Windows.Controls.Button, ICustomButton
    {
    private bool showTime = true;
    private TimeOptions options = new();

    public TimeControl(IComponentOptions timeOptions)
    {
      if (timeOptions != null)
        options = (TimeOptions)timeOptions;
      showTime = options.Display == TimeDisplay.Time;
      
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    public void Refresh()
    {
      if (showTime)
        TimeLabel.Text = DateTime.Now.ToString(options.TimeFormat);
      else
        TimeLabel.Text = DateTime.Now.ToString(options.DateFormat);
    }

    public void CustomButton_Click(object sender, RoutedEventArgs e)
    {
      showTime = !showTime;
    }
  }
}
