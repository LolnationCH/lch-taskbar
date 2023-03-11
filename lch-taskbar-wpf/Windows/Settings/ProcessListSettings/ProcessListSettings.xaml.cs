using lch_configuration.ComponentOptions;
using lch_configuration.CustomTypes;
using System.Diagnostics;
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
      SetupList();
    }

    private void SetupList()
    {
      foreach (var processInformation in options.ProcessNamesExcluded)
        ProcessListSP.Children.Add(new ProcessLineSettings(processInformation, this));
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      ProcessInformation process = new();
      options.ProcessNamesExcluded.AddChildren(process);
      ProcessListSP.Children.Add(new ProcessLineSettings(process, this));
    }

    public void RemoveProcess(ProcessLineSettings processLine)
    {
      options.ProcessNamesExcluded.RemoveChildren(processLine.processInformation);
      ProcessListSP.Children.Remove(processLine);
    }
    
    private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      options.ProcessNamesExcluded.RemoveAllEmpty();
    }
  }
}
