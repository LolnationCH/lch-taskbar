using System.Windows.Controls;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class ShortcutsControl : StackPanel
  {
    public ShortcutsControl()
    {
      InitializeComponent();
      Refresh();
    }

    public void Refresh()
    {
      Children.Clear();

      Configuration.Configuration
        .GetInstance()
        .GetData
        .ShortcutsData
          .ForEach(x => Children.Add(new ShortcutControl(x)));
    }
  }
}
