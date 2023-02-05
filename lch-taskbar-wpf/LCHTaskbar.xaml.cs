﻿using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Media;
using lch_taskbar.Utils;
using lch_taskbar.TaskbarComponents;
using System.Windows;
using lch_configuration.Configuration;

namespace lch_taskbar
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
      SetupComponents();
      WindowsTaskbar.Hide();
      SetTaskbarPosition();
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

    private static (string, double) GetBackgroundColorConfiguration()
    {
      return (lch_configuration.Configuration.Configuration.GetInstance().GetData.BackgroundColor,
              Double.Parse(lch_configuration.Configuration.Configuration.GetInstance().GetData.Opacity));
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

    private void SetTaskbarPosition()
    {
      var position = lch_configuration.Configuration.Configuration.GetInstance().GetData.Position;

      if (position == TaskbarPosition.Top ||
          position == TaskbarPosition.Bottom)
      {
        SetTaskbarToMonitorSizeHorizontal();
      }
      else
      {
        SetTaskbarToMonitorSizeVertical();
      }

      switch (position)
      {
        case TaskbarPosition.Top:
          Top = 0;
          break;
        case TaskbarPosition.Bottom:
          Top = SystemParameters.PrimaryScreenHeight - Height;
          break;
        case TaskbarPosition.Left:
          Left = 0;
          break;
        case TaskbarPosition.Right:
          Left = SystemParameters.PrimaryScreenWidth - Width;
          break;
      }
    }

    private void SetTaskbarToMonitorSizeHorizontal()
    {
      Height = 22;
      Width = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Width;
      var widthColumn = Width / 3;
      Column1.Width = new System.Windows.GridLength(widthColumn);
      Column2.Width = new System.Windows.GridLength(widthColumn);
      Column3.Width = new System.Windows.GridLength(widthColumn);
    }

    private void SetTaskbarToMonitorSizeVertical()
    {
      Width = 22;
      Height = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds.Height;
      var heightRow = Height / 3;
      Row1.Height = new System.Windows.GridLength(heightRow);
      Row2.Height = new System.Windows.GridLength(heightRow);
      Row3.Height = new System.Windows.GridLength(heightRow);
    }

    private List<ProcessControl> GetProcessControls()
    {
      return WindowUtils.FindVisualChilds<ProcessControl>(this).Where(x => x.Name == "ProcessControl").ToList();
    }
    
    public void Refresh(string title)
    {
      GetProcessControls().ForEach(x => x.Refresh());
      SetCurrentProcessTitle(title);
    }

    private void Toggle()
    {
      ToggleOnlyThis();
      WindowsTaskbar.Toggle();
    }

    private void ToggleOnlyThis()
    {
      if (IsVisible)
        Hide();
      else
        Show();
    }

    private void Reload()
    {
      lch_configuration.Configuration.Configuration.GetInstance().Reload();
      
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
      var shortcutsControls = WindowUtils.FindVisualChilds<ShortcutsControl>(this);
      foreach (var shortcutControl in shortcutsControls)
      {
        shortcutControl.Refresh();
      }      
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      lch_configuration.Configuration.Configuration.GetInstance().Save();
      WindowsTaskbar.Show();
      base.OnClosing(e);
    }
  }
}
