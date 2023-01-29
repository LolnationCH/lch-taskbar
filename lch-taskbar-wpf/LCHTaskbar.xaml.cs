using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Media;

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
    
    private void SetupTaskbarStyle()
    {
      try
      {
        var BackgroundColor = (new BrushConverter().ConvertFrom(Configuration.Configuration.GetInstance().GetData.BackgroundColor) as SolidColorBrush)!; 
        BackgroundColor.Opacity = Double.Parse(Configuration.Configuration.GetInstance().GetData.Opacity) / 100;
        Background = BackgroundColor;
      }
      catch
      {
        var BackgroundColor = (new BrushConverter().ConvertFrom("#000000") as SolidColorBrush)!;
        BackgroundColor.Opacity = 0;
        Background = BackgroundColor;
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
    
    protected override void OnClosing(CancelEventArgs e)
    {
      WindowsTaskbar.Show();
      base.OnClosing(e);
    }
  }
}
