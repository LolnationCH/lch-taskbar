using System.Windows.Controls;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for TimeControl.xaml
  /// </summary>
  public partial class TimeControl : System.Windows.Controls.Button
    {
    private bool showTime = true;
    
    public TimeControl()
    {
      InitializeComponent();
      SetupCurrentTimeLabel();
    }

    private void SetupCurrentTimeLabel()
    {
      DispatcherTimer LiveTime = new DispatcherTimer();
      LiveTime.Interval = TimeSpan.FromSeconds(1);
      LiveTime.Tick += currentTime_Tick;
      LiveTime.Start();
    }
    private void currentTime_Tick(object? sender, EventArgs e)
    {
      if (showTime)
        Content = DateTime.Now.ToString("HH:mm:ss");
      else
        Content = DateTime.Now.ToString("dd/MM/yyyy");
    }

    private void CurrentTime_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      showTime = !showTime;
    }
  }
}
