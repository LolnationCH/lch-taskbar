using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {

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
        CurrentTime.Content = DateTime.Now.ToString("HH:mm:ss");
      else
        CurrentTime.Content = DateTime.Now.ToString("dd/MM/yyyy");
    }

    private void CurrentTime_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      showTime = !showTime;
    }
  }
}
