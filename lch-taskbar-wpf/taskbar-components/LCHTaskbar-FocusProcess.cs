using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    protected void CheckProcessInformations()
    {
      var process = Utils.GetAllProcessInformations().OrderBy(x => x.ProcessName);
      var icons = process.Select(x => CreateIconForProcess(x));
      Dispatcher.Invoke(() =>
      {
        leftSP.Children.Clear();
        foreach (var icon in icons)
        {
          if (icon != null)
            leftSP.Children.Add(icon);
        }
      });
    }

    private void LaunchProcessByIndex(int index)
    {
      var i = leftSP.Children.Count <= index || index == -1 ? leftSP.Children.Count - 1 : index;
      var tag = (leftSP.Children[i] as FrameworkElement)!.Tag;
      if (tag == null)
        return;

      var processInformation = tag as ProcessInformation;
      if (processInformation == null)
        return;

      Utils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }

    protected void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
    {
      CheckProcessInformations();

      AutomationElement? focusedElement = sender as AutomationElement;
      if (focusedElement != null)
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
    }
    
    protected void SetCurrentProcessTitle(string title)
    {
      Dispatcher.Invoke(() =>
      {
        MiddleContent.Content = title;
      });
    }
  }
}
