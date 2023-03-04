using lch_taskbar.Utils;
using lch_taskbar_wpf.TaskbarComponents;
using lch_taskbar_wpf.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace lch_taskbar.TaskbarComponents
{
  public partial class ProcessControl : StackPanel
  {
    public ProcessControl()
    {
      InitializeComponent();
      Refresh();
    }
    
    public void Refresh()
    {
      Orientation = ControlsUtils.GetOrientationBasedOnConfig();
      var process = ProcessUtils.GetAllUniqueProcessInformation().OrderBy(x => x.ProcessName);
      var processButtons = process.Select(x => new ProcessButton(x));
      Dispatcher.Invoke(() =>
      {
        Children.Clear();
        foreach (var processButton in processButtons)
        {
          if (processButton != null)
            Children.Add(processButton);
        }
      });
    }
    public void LaunchProcessByIndex(int index)
    {
      var i = Children.Count <= index || index == -1 ? Children.Count - 1 : index;
      var processInformation = (Children[i] as ProcessButton)!.processInformation;
      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
