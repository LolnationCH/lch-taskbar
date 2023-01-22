using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Automation;
using System.Windows.Interop;
using System.Windows.Threading;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    private HwndSource? source;

    public LCHTaskbar()
    {
      InitializeComponent();

      Setup();
    }

    private void Setup()
    {
      WindowsTaskbar.Hide();
      SetTaskbarToMonitorSize();
    }

    private void SetTaskbarToMonitorSize()
    {
      Width = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Width;
    }
    
    public void Refresh(string title)
    {
      ProcessSP.Refresh();
      SetCurrentProcessTitle(title);
    }
    public void SetCurrentProcessTitle(string title)
    {
      Dispatcher.Invoke(() =>
      {
        MiddleContent.Content = title;
      });
    }

    private void Toggle()
    {
      if (this.IsVisible)
        this.Hide();
      else
        this.Show();
      WindowsTaskbar.Toggle();
    }
    protected override void OnClosing(CancelEventArgs e)
    {
      WindowsTaskbar.Show();
      base.OnClosing(e);
    }
  }
}
