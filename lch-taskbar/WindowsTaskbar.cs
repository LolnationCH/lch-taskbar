using System.Runtime.InteropServices;

public static class WindowsTaskbar
{
  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  private static extern IntPtr FindWindow(
      string lpClassName,
      string lpWindowName);

  [DllImport("user32.dll", SetLastError = true)]
  private static extern int SetWindowPos(
      IntPtr hWnd,
      IntPtr hWndInsertAfter,
      int x,
      int y,
      int cx,
      int cy,
      uint uFlags
  );

  [Flags]
  private enum SetWindowPosFlags : uint
  {
    HideWindow = 128,
    ShowWindow = 64
  }

  private const string taskbarClassName = "Shell_TrayWnd";

  private static bool _isHidden = false;

  public static void Show()
  {
    var window = FindWindow(taskbarClassName, "");
    SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, (uint)SetWindowPosFlags.ShowWindow);
    _isHidden = false;
  }

  public static void Hide()
  {
    var window = FindWindow(taskbarClassName, "");
    SetWindowPos(window, IntPtr.Zero, 0, 0, 0, 0, (uint)SetWindowPosFlags.HideWindow);
    _isHidden = true;
  }

  public static void Toggle()
  {
    if (_isHidden)
      Show();
    else
      Hide();
  }

}