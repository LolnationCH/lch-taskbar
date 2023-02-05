using lch_configuration.ComponentOptions;
using System.Windows;

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

    private void SetupOptionsField()
    {      
      var container = new DynamicOptionContainer(_componentOptions);
      MainContainer.Children.Add(container);
    }
  }
}
