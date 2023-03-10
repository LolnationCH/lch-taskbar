using lch_configuration.ComponentOptions;
using lch_taskbar.TaskbarComponents;
using System.Diagnostics;
using System.Windows.Controls;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class CurrentProcessTitleControl : UserControl
  {
    TitleOptions options = new();
    public CurrentProcessTitleControl(IComponentOptions? Options)
    {
      if (Options is TitleOptions titleOptions)
        options = titleOptions;
      
      InitializeComponent();
    }

    public void Refresh(Process process)
    {
      TextBlock.Text = FormatText(process);
    }

    private string FormatText(Process process)
    {
      var text = options.TitleFormat;
      text = text.Replace("{title}", process.MainWindowTitle);
      text = text.Replace("{process}", process.ProcessName);
      text = text.Replace("{process-id}", process.Id.ToString());
      text += process.Responding? "": "(Not responding)";
      return text;
    }
  }
}
