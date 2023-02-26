using lch_configuration.ComponentOptions;
using lch_configuration.Configuration;
using lch_taskbar.TaskbarComponents;
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
    public void SetCurrentProcessTitle(string title)
    {
      Dispatcher.Invoke(() =>
      {
        var control = WindowUtils.FindVisualChilds<ConfiguredTextBlock>(this).Where(x => x.Name == "MiddleContent").FirstOrDefault();
        if (control == null)
          return;
        control.Text = title;
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
      // Modify this snippet from lch-taskbar\TaskbarComponents\ComponentsDictionary.cs:
      return name?.ToLower() switch
      {
        "bluetooth" => new BluetoothControl(),
        "network" => new NetworkControl(),
        "process" or "processes" => new ProcessControl(),
        "volume" or "sound" => new SoundControl(),
        "shortcuts" => new ShortcutsControl(options),
        "spotify" => new SpotifyControl(),
        "time" or "date" => new TimeControl(),
        "weather" => new WeatherControl(options!),
        "title" => new ConfiguredTextBlock()
        {
          Name = "MiddleContent",
          Text = "Initializing...",
          HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
          VerticalAlignment = System.Windows.VerticalAlignment.Center,
        },
        _ => null,
      };
    }
  }
}
