using lch_configuration.ComponentOptions;
using System.Windows;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ProcessListSettings : Window
  {
    ProcessOptions options = new();
    
    public ProcessListSettings(object? Options)
    {
      if (Options is ProcessOptions ProcessOptions)
        options = ProcessOptions;
      
      InitializeComponent();
    }
  }
}
