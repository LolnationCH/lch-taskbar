using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

    private void SetupOptionsField()
    {
      var type = _componentOptions.GetType();
      type.GetProperties().ToList().ForEach(property =>
      {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Pixel) });
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

        Children.Add(grid);
      });
    }
  }
}
