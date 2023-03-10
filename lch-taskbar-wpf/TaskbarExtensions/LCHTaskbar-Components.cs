using lch_configuration.ComponentOptions;
using lch_configuration.Configuration;
using lch_taskbar.TaskbarComponents;
using lch_taskbar_wpf.TaskbarComponents;
using System.Windows;
using System.Windows.Controls;

namespace lch_taskbar
{
  public partial class LCHTaskbar
  {
    public void SetupComponents()
    {
      RemoveAllComponents();
      if (Configuration.GetInstance().GetData.Position == TaskbarPosition.Top ||
          Configuration.GetInstance().GetData.Position == TaskbarPosition.Bottom)
      {
        Configuration.GetInstance().GetData.ComponentList.GetLeftComponents().ForEach(x => AddComponent(x, leftSP));
        Configuration.GetInstance().GetData.ComponentList.GetMiddleComponents().ForEach(x => AddComponent(x, middleSP));
        Configuration.GetInstance().GetData.ComponentList.GetRightComponents().ForEach(x => AddComponent(x, rightSP));
      }
      else
      {
        Configuration.GetInstance().GetData.ComponentList.GetLeftComponents().ForEach(x => AddComponent(x, topSP));
        Configuration.GetInstance().GetData.ComponentList.GetMiddleComponents().ForEach(x => AddComponent(x, centerSP));
        Configuration.GetInstance().GetData.ComponentList.GetRightComponents().ForEach(x => AddComponent(x, bottomSP));
      }
    }
    public void SetCurrentProcessTitle(int processId)
    {
      using System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
      Dispatcher.Invoke(() =>
      {
        var controls = WindowUtils.FindVisualChilds<CurrentProcessTitleControl>(this);
        foreach (var control in controls)
          control.Refresh(process);
      });
    }

    private void RemoveAllComponents()
    {
      leftSP.Children.Clear();
      middleSP.Children.Clear();
      rightSP.Children.Clear();

      topSP.Children.Clear();
      centerSP.Children.Clear();
      bottomSP.Children.Clear();
    }

    private static void AddComponent(Component element, StackPanel stackPanel)
    {
      var uIElement = GetComponentsByName(element.Name, element.Options);
      if (uIElement == null)
        return;

      stackPanel.Children.Add(uIElement);
    }

    private static UIElement? GetComponentsByName(string? name, IComponentOptions? options)
    {
      // Also modify the ComponentFactory
      return name?.ToLower() switch
      {
        "input" => new InputControl(options),
        "bluetooth" => new BluetoothControl(),
        "network" => new NetworkControl(options),
        "process" or "processes" => new ProcessControl(options),
        "volume" or "sound" => new SoundControl(options),
        "shortcuts" => new ShortcutsControl(options),
        "spotify" => new SpotifyControl(options),
        "time" or "date" => new TimeControl(options),
        "weather" => new WeatherControl(options),
        "title" => new CurrentProcessTitleControl(options),
        _ => null,
      };
    }
  }
}
