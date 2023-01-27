using System.Windows;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for EverythingControl.xaml
  /// </summary>
  public partial class EverythingControl : System.Windows.Controls.Button
    {
    public EverythingControl()
    {
      InitializeComponent();
    }

    private void EverythingButton_Click(object sender, RoutedEventArgs e)
    {
      // Launch the everything executable
      System.Diagnostics.Process.Start("C:\\Program Files\\Everything\\Everything.exe");
    }
  }
}
