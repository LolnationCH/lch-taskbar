using lch_configuration.CustomTypes;
using lch_taskbar.Utils;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ProcessLineSettings : UserControl
  {
    ProcessListSettings mParent;
    private readonly ProcessInformation OldProcessInformation;
    public ProcessInformation processInformation;
    private List<lch_taskbar.ProcessInformation> processList = ProcessUtils.GetAllProcessInformation();

    public ProcessLineSettings(ProcessInformation process, ProcessListSettings parent)
    {
      mParent = parent;
      OldProcessInformation = process;
      processInformation = process;
      
      InitializeComponent();
      SetupComboBoxItemSource();
    }

    private void SetupComboBoxItemSource()
    {
      foreach (var process in processList)
        ProcessComboBox.Items.Add(process.ProcessName);

      if (!ProcessComboBox.Items.Contains(processInformation.Name))
        ProcessComboBox.Items.Add(processInformation.Name);
      
      ProcessComboBox.SelectedItem = processInformation.Name;
    }

    private void ProcessComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var item = ProcessComboBox.SelectedItem;
      if (item is string processName)
      {
        var pr = processList.Where(x => x.ProcessName == processName).FirstOrDefault();
        if (pr != null)
        {
          processInformation.Name = pr.ProcessName;
          processInformation.Path = ProcessUtils.GetProcessFileName(pr.ProcessHwnd);
        }
        else
        {
          processInformation = OldProcessInformation;
        }        
      }
    }

    private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      mParent.RemoveProcess(this);
    }
  }
}
