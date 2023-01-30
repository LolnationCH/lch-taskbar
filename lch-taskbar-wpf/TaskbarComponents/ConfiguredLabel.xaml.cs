using lch_taskbar_wpf.Utils;
using System.Windows.Controls;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class ConfiguredLabel : System.Windows.Controls.Label
    {
    public ConfiguredLabel()
    {
      InitializeComponent();
      SetupLabel();
    }

    private void SetupLabel()
    {
      Foreground = ColorUtils.GetSolidColorBrushFromHex(Configuration.Configuration.GetInstance().GetData.FontColor);
      FontFamily = new System.Windows.Media.FontFamily(Configuration.Configuration.GetInstance().GetData.FontFamily);
      FontSize = Double.Parse(Configuration.Configuration.GetInstance().GetData.FontSize);
    }

    public void Refresh()
    {
      SetupLabel();
    }
  }
}
