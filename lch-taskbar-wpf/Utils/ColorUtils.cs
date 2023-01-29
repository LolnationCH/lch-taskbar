using System.Windows.Media;

namespace lch_taskbar_wpf.Utils
{
  public static class ColorUtils
  {
    public static SolidColorBrush? GetSolidColorBrushFromHex(string hex)
    {
      return (new BrushConverter().ConvertFrom(hex) as SolidColorBrush);
    }

    public static SolidColorBrush? GetSolidColorBrushFromHex(string hex, double opacity)
    {
      var color = new BrushConverter().ConvertFrom(hex) as SolidColorBrush;
      if (color == null)
        return null;

      color.Opacity = opacity / 100;
      return color;
    }
  }
}
