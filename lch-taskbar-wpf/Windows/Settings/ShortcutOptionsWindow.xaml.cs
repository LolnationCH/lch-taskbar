using lch_configuration.ComponentOptions;
using System.Windows;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ShortcutOptionsWindow : Window
  {
    private ShortcutOptions _shortcutOptions;
    public ShortcutOptionsWindow(ShortcutOptions shortcutOptions)
    {
      _shortcutOptions = shortcutOptions;
      InitializeComponent();
      SetupFields();
    }

    public ShortcutOptions GetShortcutOptions()
    {
      return _shortcutOptions;
    }

    private void AddComponentButton_Click(object sender, RoutedEventArgs e)
    {
      var shortcutData = new ShortcutData();
      _shortcutOptions.Data.Add(shortcutData);
      var shortcutDataLine = new ShortcutDataLine(shortcutData, this);
      MainContainer.Children.Add(shortcutDataLine);
    }

    private void SetupFields()
    {
      _shortcutOptions.Data.ForEach(data =>
      {
        var shortcutDataLine = new ShortcutDataLine(data, this);
        MainContainer.Children.Add(shortcutDataLine);
      });
    }

    public void RemoveShortcutDataLine(ShortcutDataLine shortcutDataLine)
    {
      _shortcutOptions.Data.Remove(shortcutDataLine.GetShortcutData());
      MainContainer.Children.Remove(shortcutDataLine);
    }

    public void MoveUpComponentLine(ShortcutDataLine shortcutDataLine)
    {
      var index = MainContainer.Children.IndexOf(shortcutDataLine);
      if (index == 0)
      {
        return;
      }
      MainContainer.Children.Remove(shortcutDataLine);
      MainContainer.Children.Insert(index - 1, shortcutDataLine);
    }

    public void MoveDownComponentLine(ShortcutDataLine shortcutDataLine)
    {
      var index = MainContainer.Children.IndexOf(shortcutDataLine);
      if (index == MainContainer.Children.Count - 1)
      {
        return;
      }
      MainContainer.Children.Remove(shortcutDataLine);
      MainContainer.Children.Insert(index + 1, shortcutDataLine);
    }

  }
}
