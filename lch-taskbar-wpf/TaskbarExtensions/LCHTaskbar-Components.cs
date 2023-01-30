using lch_taskbar_wpf.TaskbarComponents;
using System.Windows;
using System.Windows.Controls;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar
  {
    public void SetupComponents()
    {
      RemoveAllComponents();
      Configuration.Configuration.GetInstance().GetData.ComponentList.GetLeftComponents().ForEach(x => AddComponent(x, leftSP));
      Configuration.Configuration.GetInstance().GetData.ComponentList.GetMiddleComponents().ForEach(x => AddComponent(x, middleSP));
      Configuration.Configuration.GetInstance().GetData.ComponentList.GetRightComponents().ForEach(x => AddComponent(x, rightSP));
    }
    public void SetCurrentProcessTitle(string title)
    {
      var controls = WindowUtils.FindVisualChilds<ConfiguredLabel>(this);
      Dispatcher.Invoke(() =>
      {
        controls.Where(x => x.Name == "MiddleContent").FirstOrDefault()!.Content = title;
      });
    }

    private void RemoveAllComponents()
    {
      leftSP.Children.Clear();
      middleSP.Children.Clear();
      rightSP.Children.Clear();
    }

    private static void AddComponent(string elementName, StackPanel stackPanel)
    {
      var element = GetComponentsByName(elementName);
      if (element == null)
        return;
      
      stackPanel.Children.Add(element);
    }

    private static UIElement? GetComponentsByName(string name)
    {
      return name.ToLower() switch
      {
        "bluetooth" => new BluetoothControl()
        {
          Margin = new Thickness(-5, 0, -3, 2)
        },
        "everything" => new EverythingControl()
        {
          Margin = new Thickness(0, 0, 0, 2)
        },
        "network" => new NetworkControl()
        {
          Margin = new Thickness(0, -1, 0, 0)
        },
        "process" or "processes" => new ProcessControl()
        {
          Margin = new Thickness(0, -3, 0, 0)
        },
        "volume" or "sound" => new SoundControl()
        {
          Margin = new Thickness(0, -5, 0, -2)
        },
        "spotify" => new SpotifyControl()
        {
          Margin = new Thickness(0, -2, 5, 0)
        },
        "time" or "date" => new TimeControl()
        {
          Margin = new Thickness(0, -3, 0, 0)
        },
        "weather" => new WeatherControl()
        {
          Margin = new Thickness(0, -3, 0, 0)
        },
        "title" => new ConfiguredLabel()
        {
          Name = "MiddleContent",
          Content = "Initializing...",
          Margin = new Thickness(0, -1, 0, 0),
          HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
          VerticalAlignment = System.Windows.VerticalAlignment.Center,
        },
        _ => null,
      };
    }
  }
}
