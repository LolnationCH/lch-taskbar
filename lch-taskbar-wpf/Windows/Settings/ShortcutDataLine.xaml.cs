using lch_configuration.ComponentOptions;
using System.Windows;
using System.Windows.Controls;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ShortcutDataLine : Border
  {
    private ShortcutData _shortcutData;
    private ShortcutOptionsWindow _parent;
    public ShortcutDataLine(ShortcutData shortcutData, ShortcutOptionsWindow window)
    {
      InitializeComponent();
      _shortcutData = shortcutData;
      _parent = window;
      var dynamicOptionContainer = new DynamicOptionContainer(_shortcutData);
      MainContainer.Children.Add(dynamicOptionContainer);
    }
    public ShortcutData GetShortcutData()
    {
      return _shortcutData;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
      _parent.RemoveShortcutDataLine(this);
    }

    private void MoveUp_Click(object sender, RoutedEventArgs e)
    {
      _parent.MoveUpComponentLine(this);
    }

    private void MoveDown_Click(object sender, RoutedEventArgs e)
    {
      _parent.MoveDownComponentLine(this);
    }
  }
}
