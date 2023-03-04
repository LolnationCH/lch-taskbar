using lch_configuration.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
  }
}
