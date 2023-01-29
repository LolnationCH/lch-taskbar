using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Media;
using lch_taskbar_wpf.Utils;
using lch_taskbar_wpf.TaskbarComponents;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    private HwndSource? source;

    public LCHTaskbar()
    {
      InitializeComponent();

      Setup();
    }

    private void Setup()
    {
      WindowsTaskbar.Hide();
      SetTaskbarToMonitorSize();
      SetupDispatcherTimer();
      SetupTaskbarStyle();
    }

    private void SetupDispatcherTimer()
    {
      DispatcherTimer LiveTime = new()
      {
        Interval = TimeSpan.FromSeconds(1)
      };
      LiveTime.Tick += ControlsTimer_Tick;
      LiveTime.Start();
    }

    private void ControlsTimer_Tick(object? sender, EventArgs e)
    {
      foreach (var control in leftSP.Children)
      {
        if (control is TaskbarComponents.ICustomButton)
        {
          (control as TaskbarComponents.ICustomButton)!.Refresh();
        }
      }
      
      foreach (var control in rightSP.Children)
      {
        if (control is TaskbarComponents.ICustomButton)
        {
          (control as TaskbarComponents.ICustomButton)!.Refresh();
        }
      }
    }

    private (string, double) GetBackgroundColorConfiguration()
    {
      return (Configuration.Configuration.GetInstance().GetData.BackgroundColor,
              Double.Parse(Configuration.Configuration.GetInstance().GetData.Opacity));
    }
    
    private void SetupTaskbarStyle()
    {
      try
      {
        (var color, var opacity) = GetBackgroundColorConfiguration();
        var BackgroundColor = ColorUtils.GetSolidColorBrushFromHex(color, opacity);
        if (BackgroundColor == null)
        {
          MessageBox.Show("Cannot parse the background color in the configuration file");
          throw new Exception();
        }
        Background = BackgroundColor;
      }
      catch
      {
        Background = new SolidColorBrush()
        {
          Color = Colors.White,
          Opacity = 0
        };
      }
    }

    private void SetTaskbarToMonitorSize()
    {
      Width = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Width;
      var widthColumn = Width / 3;
      Column1.Width = new System.Windows.GridLength(widthColumn);
      Column2.Width = new System.Windows.GridLength(widthColumn);
      Column3.Width = new System.Windows.GridLength(widthColumn);
    }
    
    public void Refresh(string title)
    {
      ProcessSP.Refresh();
      SetCurrentProcessTitle(title);
    }
    public void SetCurrentProcessTitle(string title)
    {
      Dispatcher.Invoke(() =>
      {
        MiddleContent.Content = title;
      });
    }

    private void Toggle()
    {
      if (this.IsVisible)
        this.Hide();
      else
        this.Show();
      WindowsTaskbar.Toggle();
    }

    private void ToggleOnlyThis()
    {
      if (this.IsVisible)
        this.Hide();
      else
        this.Show();
    }

    private void Reload()
    {
      Configuration.Configuration.GetInstance().Reload();
      Setup();
      var configuredLabels = WindowUtils.FindVisualChilds<ConfiguredLabel>(this);
      foreach (var label in configuredLabels)
      {
        label.Refresh();
      }
      var weatherControls = WindowUtils.FindVisualChilds<WeatherControl>(this);
      foreach (var weatherControl in weatherControls)
      {
        weatherControl.Refresh();
      }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      WindowsTaskbar.Show();
      base.OnClosing(e);
    }
  }
}
