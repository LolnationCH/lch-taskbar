using lch_taskbar.Utils;
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
      var process = ProcessUtils.GetAllProcessInformation().OrderBy(x => x.ProcessName);
      var icons = process.Select(x => CreateIconForProcess(x));
      Dispatcher.Invoke(() =>
      {
        Children.Clear();
        foreach (var icon in icons)
        {
          if (icon != null)
            Children.Add(icon);
        }
      });
    }
    public void LaunchProcessByIndex(int index)
    {
      var i = Children.Count <= index || index == -1 ? Children.Count - 1 : index;
      var tag = (Children[i] as FrameworkElement)!.Tag;
      if (tag == null)
        return;

      if (tag is not ProcessInformation processInformation)
        return;

      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }

    private System.Windows.Controls.Image CreateIconForProcess(ProcessInformation processInformation)
    {
      var image = new System.Windows.Controls.Image()
      {
        Source = Imaging.CreateBitmapSourceFromHIcon(processInformation.ProcessIcon!.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()),
        Tag = processInformation,
        Width = 12,
        Height = 12,
        Margin = new Thickness(5),
        ToolTip = processInformation.ProcessName,
      };
      image.MouseDown += Icon_MouseDown;
      return image;
    }

    private void Icon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (sender is not System.Windows.Controls.Image image)
        return;

      if (image.Tag is not ProcessInformation processInformation)
        return;

      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
