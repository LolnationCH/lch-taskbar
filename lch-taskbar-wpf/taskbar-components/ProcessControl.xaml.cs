using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace lch_taskbar_wpf.taskbar_components
{
  /// <summary>
  /// Interaction logic for ProcessControl.xaml
  /// </summary>
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

      var processInformation = tag as ProcessInformation;
      if (processInformation == null)
        return;

      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
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

      WindowUtils.SwitchToThisWindow(processInformation.ProcessHwnd);
    }
  }
}
