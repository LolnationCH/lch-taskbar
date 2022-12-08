using System.Runtime.InteropServices;
using System.Text;

public class ProgramWindowData
{
  public string Title { get; set; } = string.Empty;
  public IntPtr Handle { get; set; }
  public Rectangle Bounds { get; set; }

  public override string ToString()
  {
    return $"{Title} - {Handle} - {Bounds}";
  }

  [DllImport("user32.dll", SetLastError = true)]
  static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

  [DllImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);
  [DllImport("user32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
  [DllImport("user32.dll")]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  static extern bool GetWindowRect(HandleRef hWnd, out Rectangle lpRect);
  [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
  static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

  public void ResetPosition()
  {
    if (GetPlacement().ShowCmd != ShowWindowCommands.ShowMaximized)
      return;

    ShowWindow(Handle, (int)ShowWindowCommands.Normal);
    GetWindowRect(new HandleRef(this, Handle), out Rectangle rect);
    Bounds = rect;
  }

  public void SetPosition(int x, int y, int width, int height)
  {
    ResetPosition();
    SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, 0x0040);
  }

  public void GetExecutable()
  {
    StringBuilder sb = new StringBuilder(1024);
    GetWindowModuleFileName(Handle, sb, (uint)sb.Capacity);
    Title = sb.ToString();
    Console.WriteLine(Title.ToString());
  }

  public void SetPlacement(WINDOWPLACEMENT placement)
  {
    SetWindowPlacement(Handle, ref placement);
  }

  public WINDOWPLACEMENT GetPlacement()
  {
    WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
    GetWindowPlacement(Handle, ref placement);
    return placement;
  }
}