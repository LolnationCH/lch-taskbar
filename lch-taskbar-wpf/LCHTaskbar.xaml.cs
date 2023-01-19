using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Linq;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    private bool showTime = true; 
    private HwndSource? source;

    public LCHTaskbar()
    {
      InitializeComponent();

      Setup();
    }

    private void Setup()
    {
      WindowsTaskbar.Hide();
      this.Width = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Width;
      SetupCurrentTimeLabel();
      CheckProcessInformations();

      AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
      Automation.AddAutomationFocusChangedEventHandler(focusHandler);
    }

    private System.Windows.Controls.Image CreateIconForProcess(ProcessInformation processInformation)
    {
      var image = new System.Windows.Controls.Image();
      image.Source = Imaging.CreateBitmapSourceFromHIcon(processInformation.ProcessIcon!.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      image.Width = 12;
      image.Height = 12;
      image.Margin = new System.Windows.Thickness(5);
      image.ToolTip = processInformation.ProcessName;
      image.Tag = processInformation;
      image.MouseDown += Icon_MouseDown;
      return image;
    }

    private void Icon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      var image = sender as System.Windows.Controls.Image;
      if (image == null)
        return;

      ProcessInformation? processInformation = image.Tag as ProcessInformation;
      if (processInformation == null)
        return;

      Utils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
