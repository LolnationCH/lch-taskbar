using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace lch_taskbar.TaskbarComponents
{
  public partial class TimeControl : System.Windows.Controls.Button, ICustomButton
    {
    private bool showTime = true;
    
    public TimeControl()
    {
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    public void Refresh()
    {
      if (showTime)
        TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
      else
        TimeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    public void CustomButton_Click(object sender, RoutedEventArgs e)
    {
      showTime = !showTime;
    }
  }
}
