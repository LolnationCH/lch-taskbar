using lch_taskbar;
using lch_taskbar_wpf.Utils;
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
    string format;
    public ProcessButton(ProcessInformation processInformation, string format)
    {
      this.processInformation = processInformation;
      this.format = format;
      
      InitializeComponent();
      Setup();
    }

    private void Setup()
    {      
      var indexOfIcon = format.IndexOf("{icon}");
      var indexOfName = format.IndexOf("{name}");
      if (indexOfIcon == -1 && indexOfName == -1)
      {
        Content = format;
      }
      else if (indexOfIcon == -1)
      {
        SetupText();
        ProcessIcon.Visibility = Visibility.Collapsed;
      }
      else if (indexOfName == -1)
      {
        SetupImage();
        ProcessName.Visibility = Visibility.Collapsed;
      }
      else
      {
        if (indexOfIcon < indexOfName)
        {
          SetupImage();
          SetupText();
        }
        else
        {
          SetupText();
          SetupImage();
        }
      }
    }

    private void SetupImage()
    {
      ProcessIcon.Source = Imaging.CreateBitmapSourceFromHIcon(processInformation.ProcessIcon!.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      ProcessIcon.Tag = processInformation;
      ProcessIcon.ToolTip = processInformation.ProcessName;
    }

    private void SetupText()
    {
      ProcessName.Text = processInformation.ProcessName.Length > 20 ? processInformation.ProcessName.Substring(0, 20) + "..." : processInformation.ProcessName;
      ProcessName.Tag = processInformation;
      ProcessName.ToolTip = processInformation.ProcessName;
    }

    private void Shortcut_Click(object sender, RoutedEventArgs e)
    {
      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
