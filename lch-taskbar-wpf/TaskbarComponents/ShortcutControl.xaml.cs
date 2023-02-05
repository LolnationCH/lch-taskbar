using lch_configuration.ComponentOptions;
using lch_taskbar.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace lch_taskbar.TaskbarComponents
{
    public partial class ShortcutControl : Button
  {
    private readonly ShortcutData _shortcutData;
    
    public ShortcutControl(ShortcutData shortcutData)
    {
      InitializeComponent();
      _shortcutData = shortcutData;
      SetupButton();
    }

    private void SetupButton()
    {
      if (System.IO.File.Exists(_shortcutData.Path))
      {
        Image Icon = new()
        {
          Tag = _shortcutData.Path,
          MaxWidth = 12,
          MaxHeight = 12,
          ToolTip = _shortcutData.Name,
        };
        
        if (_shortcutData.IconPath != null)
        {
          if (System.IO.File.Exists(_shortcutData.IconPath))
          {
            Icon.Source = new BitmapImage(new Uri(_shortcutData.IconPath));
          }
        }
        else
        {
          var icon = ProcessUtils.IconFromFilePath(_shortcutData.Path);
          if (icon == null)
            return;

          Icon.Source = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        Content = Icon;
        ToolTip = _shortcutData.Name;
      }
    }

    private void Shortcut_Click(object sender, RoutedEventArgs e)
    {
      var newProcess = new Process();
      newProcess.StartInfo.FileName = _shortcutData.Path;
      newProcess.StartInfo.Arguments = _shortcutData.Arguments;
      newProcess.StartInfo.UseShellExecute = true;
      newProcess.Start();
    }
  }
}
