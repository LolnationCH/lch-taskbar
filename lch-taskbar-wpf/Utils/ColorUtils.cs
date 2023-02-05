using System.Windows.Media;

namespace lch_taskbar.Utils
{
  public static class ColorUtils
  {
    public static SolidColorBrush? GetSolidColorBrushFromHex(string hex)
    {
      return (new BrushConverter().ConvertFrom(hex) as SolidColorBrush);
    }

    public static SolidColorBrush? GetSolidColorBrushFromHex(string hex, double opacity)
    {
      if (new BrushConverter().ConvertFrom(hex) is not SolidColorBrush color)
        return null;

      color.Opacity = opacity / 100;
      return color;
    }
    public static string GetHexColor(Color? color)
    {
      if (color == null)
        return "#000000";

      return $"#{color.Value.R:X2}{color.Value.G:X2}{color.Value.B:X2}";
    }
  }
}
