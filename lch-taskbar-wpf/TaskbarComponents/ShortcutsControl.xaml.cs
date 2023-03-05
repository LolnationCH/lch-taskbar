using lch_configuration.ComponentOptions;
using lch_taskbar_wpf.Utils;
using System.Windows.Controls;

namespace lch_taskbar.TaskbarComponents
{
  public partial class ShortcutsControl : Border
  {
    private readonly ShortcutOptions options = new();
    public ShortcutsControl(IComponentOptions? Options)
    {
      InitializeComponent();

      if (Options is ShortcutOptions shortcutOptions)
        options = shortcutOptions;

      Refresh();
    }

    public void Refresh()
    {
      MainContent.Children.Clear();
      MainContent.Orientation = ControlsUtils.GetOrientationBasedOnConfig();
      options.Data.ForEach(x => MainContent.Children.Add(new ShortcutControl(x)));
    }
  }
}
