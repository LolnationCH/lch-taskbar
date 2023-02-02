namespace lch_taskbar_wpf.Utils
{
  public static class FontUtils
  {
    public static List<string> GetFontFamilies()
    {
      // Find all install font on the computer
      var fonts = new System.Drawing.Text.InstalledFontCollection();
      var fontFamilies = fonts.Families.Select(f => f.Name).ToList();
      return fontFamilies;
    }

    public static List<string> GetFontSize()
    {
      return new List<int>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 }.Select(x => x.ToString()).ToList();
    }
  }
}
