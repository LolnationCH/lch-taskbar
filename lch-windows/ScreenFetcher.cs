using System.Runtime.InteropServices;
using System.Text;

public static class ScreenFetcher
{
  private static readonly List<string> _programNamesToIgnore = new List<string>(){
    "Windows Input Experience",
    "Windows Shell Experience Host",
    "Settings",
    "Setup",
    "Program Manager",
    "Taskbar",
  }.Select(x => x.ToLower()).ToList();

  public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

  [DllImport("user32.dll")]
  public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

  [DllImport("user32.Dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  static extern bool IsWindowVisible(IntPtr hWnd);

  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  static extern int GetWindowTextLength(IntPtr hWnd);
  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  static extern bool GetWindowRect(HandleRef hWnd, out Rectangle lpRect);


  public static List<IntPtr> GetRootWindowsOfProcess(int pid)
  {
    List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
    List<IntPtr> dsProcRootWindows = new();
    foreach (IntPtr hWnd in rootWindows)
    {
      uint lpdwProcessId;
      GetWindowThreadProcessId(hWnd, out lpdwProcessId);
      if (lpdwProcessId == pid)
        dsProcRootWindows.Add(hWnd);
    }
    return dsProcRootWindows;
  }

  public static string GetWindowTitle(IntPtr hWnd)
  {
    StringBuilder sb = new(GetWindowTextLength(hWnd) + 1);
    GetWindowText(hWnd, sb, sb.Capacity);
    return sb.ToString();
  }

  private static bool IgnoreProgram(IntPtr hWnd)
  {
    var Title = GetWindowTitle(hWnd);
    if (Title.Length == 0 || _programNamesToIgnore.Contains(Title.ToLower()))
      return true;
    return false;
  }

  private static void GetWindowData(IntPtr hWnd, List<ScreenData> screens)
  {
    if (!IsWindowVisible(hWnd))
      return;

    ProgramWindowData windowData = new();
    windowData.Handle = hWnd;
    windowData.Title = GetWindowTitle(hWnd);
    if (windowData.Title.Length == 0 || _programNamesToIgnore.Contains(windowData.Title.ToLower()))
      return;

    Rectangle rect;
    GetWindowRect(new HandleRef(null, hWnd), out rect);
    windowData.Bounds = rect;

    var screen = System.Windows.Forms.Screen.FromHandle(hWnd);
    screens.Where(s => s.DeviceName == screen.DeviceName).FirstOrDefault()?.AddWindow(windowData);
  }

  public static List<IntPtr> GetProcessHandle()
  {
    return GetChildWindows(IntPtr.Zero).Where(x => !IgnoreProgram(x))
                                       .ToList();
  }
  
  public static List<ScreenData> GetAllWindowsInScreens()
  {
    List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
    List<ScreenData> screens = System.Windows.Forms.Screen.AllScreens.Select(x => new ScreenData()
    {
      DeviceName = x.DeviceName,
      Bounds = x.Bounds,
      WorkingArea = x.WorkingArea,
      Primary = x.Primary,
    }).ToList();

    foreach (IntPtr hWnd in rootWindows)
    {
      uint lpdwProcessId;
      GetWindowThreadProcessId(hWnd, out lpdwProcessId);
      GetWindowData(hWnd, screens);
    }
    return screens;
  }

  public static List<IntPtr> GetChildWindows(IntPtr parent)
  {
    List<IntPtr> result = new();
    GCHandle listHandle = GCHandle.Alloc(result);
    try
    {
      Win32Callback childProc = new(EnumWindow);
      EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
    }
    finally
    {
      if (listHandle.IsAllocated)
        listHandle.Free();
    }
    return result;
  }

  private static bool EnumWindow(IntPtr handle, IntPtr pointer)
  {
    GCHandle gch = GCHandle.FromIntPtr(pointer);
    if (gch.Target is not List<IntPtr> list || list is null)
      throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");

    list.Add(handle);
    return true;
  }
}