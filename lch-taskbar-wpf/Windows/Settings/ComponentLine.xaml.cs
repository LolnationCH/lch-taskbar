using lch_configuration.ComponentOptions;
using lch_configuration.Configuration;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.Design.AxImporter;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ComponentLine : UserControl
  {
    private static readonly List<string> componentsName = new(ComponentFactory.GetOptionsByComponentName().Keys);
    private string? currentSelection;
    private IComponentOptions? currentOptions;
    private readonly ComponentContainer parent;

    public ComponentLine(ComponentContainer parent)
    {
      InitializeComponent();
      this.parent = parent;
      ComponentList_ComboBox.ItemsSource = componentsName;
    }

    public void SetComponentInformation(Component component)
    {
      ComponentList_ComboBox.SelectedItem = component.Name;
      currentOptions = component.Options;
    }

    private void MoveUp_Click(object sender, RoutedEventArgs e)
    {
      parent.MoveUpComponentLine(this);
    }
    private void MoveDown_Click(object sender, RoutedEventArgs e)
    {
      parent.MoveDownComponentLine(this);
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
      parent.RemoveComponentLine(this);
    }

    public Component? GetComponent()
    {
      return ComponentFactory.CreateComponentFromNameAndOptions(currentSelection, currentOptions);
    }

    private void ComponentList_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      currentSelection = (string)ComponentList_ComboBox.SelectedItem;
      if (currentSelection is not null)
      {
        var options = ComponentFactory.GetOptionsByComponentName()[currentSelection];
        if (options != null)
          EditButton.IsEnabled = true;
        else
          EditButton.IsEnabled = false;
        currentOptions = options;
      }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      if (currentOptions is null)
        return;
      
      switch (currentOptions)
      {
        case ShortcutOptions shortcutOptions:
          var window = new ShortcutOptionsWindow(shortcutOptions);
          window.ShowDialog();
          currentOptions = window.GetShortcutOptions();
          break;
        default:
          var dynamicWindow = new DynamicSettingsWindow(currentOptions);
          dynamicWindow.ShowDialog();
          currentOptions = dynamicWindow.GetComponentOptions();
          break;
      }
    }
  }
}
