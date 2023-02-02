using lch_configuration.ComponentOptions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class DynamicSettingsWindow : Window
  {
    private IComponentOptions _componentOptions;
    public DynamicSettingsWindow(IComponentOptions componentOptions)
    {
      InitializeComponent();
      _componentOptions = componentOptions;
      SetWindowTitle();
      SetupOptionsField();
    }

    public IComponentOptions GetComponentOptions()
    {
      return _componentOptions;
    }

    private void SetWindowTitle()
    {
      var name = _componentOptions.GetType().Name;
      name = name.Replace("Options", "");
      Title = $"{name} Options";
    }

    private string GetPropertyName(string name)
    {
      return name.Substring(0, 1).ToUpper() + name.Substring(1) + " : ";
    }

    private void SetupOptionsField()
    {
      var type = _componentOptions.GetType();
      type.GetProperties().ToList().ForEach(property =>
      {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

        var label = new Label
        {
          Content = GetPropertyName(property.Name),
          Margin = new Thickness(0, 0, 0, 5)
        };
        Grid.SetColumn(label, 0);
        
        var textBox = new TextBox
        {
          Margin = new Thickness(5),
          HorizontalAlignment = HorizontalAlignment.Stretch
        };
        textBox.SetBinding(TextBox.TextProperty, new Binding(property.Name) { Source = _componentOptions });
        Grid.SetColumn(textBox, 1);
        
        grid.Children.Add(label);
        grid.Children.Add(textBox);
        MainContainer.Children.Add(grid);
      });
    }
  }
}
