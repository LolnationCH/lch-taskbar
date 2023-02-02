using lch_configuration.ComponentOptions;
using System.Windows.Controls;

namespace lch_taskbar.TaskbarComponents
{
  public partial class ShortcutsControl : StackPanel
  {
    private readonly ShortcutDatas shortcutDatas = new();
    public ShortcutsControl(IComponentOptions? componentOptions)
    {
      InitializeComponent();
      
      if (componentOptions != null)
        shortcutDatas = (ShortcutDatas)componentOptions;
      
      Refresh();
    }

    public void Refresh()
    {
      Children.Clear();

      shortcutDatas.Data.ForEach(x => Children.Add(new ShortcutControl(x)));
    }
  }
}
