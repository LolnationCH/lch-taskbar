using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Controls.Primitives;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class DynamicOptionContainer : StackPanel
  {
    private object _componentOptions;
    public DynamicOptionContainer(object componentOptions)
    {
      _componentOptions = componentOptions;
      InitializeComponent();
      SetupOptionsField();
    }

    public object GetComponentOptions()
    {
      return _componentOptions;
    }

    private string GetPropertyName(string name)
    {
      return string.Concat(name[..1].ToUpper(), name.AsSpan(1), " : ");
    }

    private UIElement SetupInputField(PropertyInfo property)
    {
      if (property.PropertyType.BaseType?.Name == "Enum")
      {
        var comboBox = new ComboBox()
        {
          ItemsSource = property.PropertyType.GetEnumNames(),
          Margin = new Thickness(5),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          MinWidth = 100,
        };
        comboBox.SetBinding(Selector.SelectedValueProperty, new Binding(property.Name) { Source = _componentOptions });
        return comboBox;
      }
      else if (property.PropertyType.Name == "Boolean")
      {
        var checkbox = new CheckBox()
        {
          Margin = new Thickness(5),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          MinWidth = 100,
        };
        checkbox.SetBinding(ToggleButton.IsCheckedProperty, new Binding(property.Name) { Source = _componentOptions });
        return checkbox;
      }
      else
      {
        var textBox = new TextBox
        {
          Margin = new Thickness(5),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          MinWidth = 100,
        };
        textBox.SetBinding(TextBox.TextProperty, new Binding(property.Name) { Source = _componentOptions });
        return textBox;
      }
    }

    private void SetupOptionsField()
    {
      var type = _componentOptions.GetType();
      type.GetProperties().ToList().ForEach(property =>
      {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150, GridUnitType.Pixel) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

        var label = new Label
        {
          Content = GetPropertyName(property.Name),
          Margin = new Thickness(0, 0, 0, 5)
        };
        Grid.SetColumn(label, 0);

        var input = SetupInputField(property);
        Grid.SetColumn(input, 1);

        grid.Children.Add(label);
        grid.Children.Add(input);

        Children.Add(grid);
      });
    }
  }
}
