using lch_configuration.ComponentOptions;
using System.Windows.Controls;

namespace lch_taskbar.TaskbarComponents
{
  public partial class ShortcutsControl : Border
  {
    private readonly ShortcutOptions shortcutOptions = new();
    public ShortcutsControl(IComponentOptions? componentOptions)
    {
      InitializeComponent();
      
      if (componentOptions != null)
        shortcutOptions = (ShortcutOptions)componentOptions;
      
      Refresh();
    }

    public void Refresh()
    {
      MainContent.Children.Clear();
      shortcutOptions.Data.ForEach(x => MainContent.Children.Add(new ShortcutControl(x)));
    }
  }
}
