using System.ComponentModel;
using System.Diagnostics;
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

      // AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
      // Automation.AddAutomationFocusChangedEventHandler(focusHandler);
    }
    
    private void SetTaskbarToMonitorSize()
    {
      Width = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Width;
    }
    
    public void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
    {
      ProcessSP.Refresh();

      AutomationElement? focusedElement = sender as AutomationElement;
      if (focusedElement != null)
      {
        try
        {
          int processId = focusedElement.Current.ProcessId;
          var currentProcess = Process.GetCurrentProcess();
          if (processId == currentProcess.Id)
            return;

          using (Process process = Process.GetProcessById(processId))
          {
            SetCurrentProcessTitle(process.MainWindowTitle);
          }
        }
        catch (Exception)
        {
          // ignored
        }
      }
    }
    private void SetCurrentProcessTitle(string title)
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
