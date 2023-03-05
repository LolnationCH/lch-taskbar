using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Automation;

namespace lch_taskbar
{
  public partial class App : System.Windows.Application
  {

    public App()
    {
      //Create a new thread for the Automation Class
      BackgroundWorker worker = new();
      worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
      worker.RunWorkerAsync();

      // Just get the instance to load the config
      lch_configuration.Configuration.Configuration.GetInstance();
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
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

          Dispatcher.Invoke(() =>
          {
            (Current.MainWindow as LCHTaskbar)!.Refresh(processId);
          });
        }
        catch (Exception)
        {
          // ignored
        }
      }
    }
  }
}
