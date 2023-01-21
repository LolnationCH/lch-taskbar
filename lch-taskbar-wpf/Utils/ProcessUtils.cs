using lch_taskbar_wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


  public static class ProcessUtils
  {

  [DllImport("user32.dll")]
  public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  static extern bool IsWindowVisible(IntPtr hWnd);

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

  public static List<ProcessInformation> GetAllProcessInformation()
  {
    List<ProcessInformation> processInformation = new();
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
          processInformation.Add(new ProcessInformation()
          {
            ProcessIcon = icon,
            ProcessHwnd = p,
            ProcessName = ScreenFetcher.GetWindowTitle(p)
          });
      }
    }
    return processInformation;
  }
}
