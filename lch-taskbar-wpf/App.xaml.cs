using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Automation;

namespace lch_taskbar_wpf
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : System.Windows.Application
  {

    public App()
    {
      //Create a new thread for the Automation Class
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += new DoWorkEventHandler(worker_DoWork);
      worker.RunWorkerAsync();
    }

    private void worker_DoWork(object? sender, DoWorkEventArgs e)
    {
      AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
      Automation.AddAutomationFocusChangedEventHandler(focusHandler);
      while (true)
      {
        Thread.Sleep(1000);
      }
    }

    private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
    {
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
            Dispatcher.Invoke(() =>
            {
              (Current.MainWindow as LCHTaskbar)!.Refresh(process.MainWindowTitle);
            });
          }
        }
        catch (Exception)
        {
          // ignored
        }
      }
    }
  }
}
