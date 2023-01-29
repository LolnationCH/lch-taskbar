using System.Windows;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class EverythingControl : System.Windows.Controls.Button, ICustomButton
  {
    public EverythingControl()
    {
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    public void CustomButton_Click(object sender, RoutedEventArgs e)
    {
      System.Diagnostics.Process.Start(Configuration.Configuration.GetInstance().GetData.EverythingPath);
    }

    public void Refresh()
    {
    }
  }
}
