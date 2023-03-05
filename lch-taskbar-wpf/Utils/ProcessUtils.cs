using lch_taskbar;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace lch_taskbar.Utils
{
  public static class ProcessUtils
  {

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsWindowVisible(IntPtr hWnd);

    public static Icon? GetIconOfProcess(IntPtr hwnd)
    {
      if (!IsWindowVisible(hwnd))
        return null;

      var _ = GetWindowThreadProcessId(hwnd, out uint lpdwProcessId);
      if (lpdwProcessId == 0)
        return null;

      var process = Process.GetProcessById((int)lpdwProcessId);
      Icon? icon = null;

      try
      {
        if (process.MainModule != null)
          icon = IconFromFilePath(process.MainModule.FileName);
      }
      catch
      {
        return null;
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

    public static List<ProcessInformation> GetAllUniqueProcessInformation()
    {
      return GetAllProcessInformation().GroupBy(x => GetProcessFileName(x.ProcessHwnd)).Select(x => x.First()).ToList();
    }

    private static string? GetProcessFileName(nint Hwnd)
    {
      var _ = GetWindowThreadProcessId(Hwnd, out uint lpdwProcessId);
      if (lpdwProcessId == 0)
        return null;

      var process = Process.GetProcessById((int)lpdwProcessId);
      return process.MainModule?.FileName;
    }

    public static Icon? IconFromFilePath(string filePath)
    {
      Icon? result = null;

      try
      {
        result = Icon.ExtractAssociatedIcon(filePath);
      }
      catch (Exception) { }
      return result;
    }
  }
}