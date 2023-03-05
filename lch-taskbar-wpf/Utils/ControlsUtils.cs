using lch_configuration.ComponentOptions;
using lch_configuration.Configuration;
using lch_taskbar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace lch_taskbar_wpf.Utils
{
  public static class ControlsUtils
  {
    public static Orientation GetOrientationBasedOnConfig()
    {
      return Configuration.GetInstance().GetData.Position switch
      {
        TaskbarPosition.Left   => Orientation.Vertical,
        TaskbarPosition.Right  => Orientation.Vertical,
        TaskbarPosition.Top    => Orientation.Horizontal,
        TaskbarPosition.Bottom => Orientation.Horizontal,
        _ => Orientation.Horizontal
      };
    }

    public static Image GetImageFromImagePath(string Path, string ToolTip)
    {
      return new Image()
      {
        MaxWidth = 25,
        MaxHeight = 25,
        ToolTip = ToolTip,
        Source = new BitmapImage(new Uri(Path)),
      };
    }
  }
}
