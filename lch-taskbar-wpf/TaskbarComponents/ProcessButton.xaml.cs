using lch_taskbar;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for ProcessButton.xaml
  /// </summary>
  public partial class ProcessButton : Button
  {
    public ProcessInformation processInformation;
    public ProcessButton(ProcessInformation processInformation)
    {
      this.processInformation = processInformation;
      InitializeComponent();
      SetupImage();
    }

    private void SetupImage()
    {
      ProcessIcon.Source = Imaging.CreateBitmapSourceFromHIcon(processInformation.ProcessIcon!.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      ProcessIcon.Tag = processInformation;
      ProcessIcon.ToolTip = processInformation.ProcessName;
    }

    private void Shortcut_Click(object sender, RoutedEventArgs e)
    {
      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
