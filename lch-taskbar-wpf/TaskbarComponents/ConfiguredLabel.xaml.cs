using lch_taskbar.Utils;
using System.Windows.Controls;

namespace lch_taskbar.TaskbarComponents
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
      Foreground = ColorUtils.GetSolidColorBrushFromHex(lch_configuration.Configuration.Configuration.GetInstance().GetData.FontColor);
      FontFamily = new System.Windows.Media.FontFamily(lch_configuration.Configuration.Configuration.GetInstance().GetData.FontFamily);
      FontSize = Double.Parse(lch_configuration.Configuration.Configuration.GetInstance().GetData.FontSize);
    }

    public void Refresh()
    {
      SetupLabel();
    }
  }
}
