using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace lch_taskbar_wpf
{
  public static class Utils
  {
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

    public static void SwitchToThisWindow(IntPtr hWnd)
    {
      SwitchToThisWindow(hWnd, true);
    }


    public static Icon? GetIconOfProcess(IntPtr hwnd)
    {
      if (!IsWindowVisible(hwnd))
        return null;

      uint lpdwProcessId;
      GetWindowThreadProcessId(hwnd, out lpdwProcessId);
      if (lpdwProcessId == 0)
        return null;
      
      var process = Process.GetProcessById((int)lpdwProcessId);
      Icon? icon = null;
      try
      {
        if (process.MainModule != null)
          icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
      }
      catch (Exception)
      {
          // ignored
      }

      return icon;
    }

    public static List<ProcessInformation> GetAllProcessInformations()
    {
      List<ProcessInformation> processInformations = new();
      var processHwnd = ScreenFetcher.GetProcessHandle();
      foreach (var p in processHwnd)
      {
        var screen = System.Windows.Forms.Screen.FromHandle(p);
        if (screen == null)
          continue;

        if (screen.DeviceName == System.Windows.Forms.Screen.PrimaryScreen?.DeviceName)
        {
          var icon = GetIconOfProcess(p);
          if (icon != null)
            processInformations.Add(new ProcessInformation()
            {
              ProcessIcon = icon,
              ProcessHwnd = p,
              ProcessName = ScreenFetcher.GetWindowTitle(p)
            });
        }
      }
      return processInformations;
    }
  }
}
