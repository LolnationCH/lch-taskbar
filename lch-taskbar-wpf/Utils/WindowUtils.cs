using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows;

public static class WindowUtils
{
  [DllImport("user32.dll", SetLastError = true)]
  static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

  public static void SwitchToThisWindow(IntPtr hWnd)
  {
    SwitchToThisWindow(hWnd, true);
  }

  [DllImport("user32.dll")]
  public static extern int SetWindowLong(IntPtr window, int index, int value);
  [DllImport("user32.dll")]
  public static extern int GetWindowLong(IntPtr window, int index);


  const int GWL_EXSTYLE = -20;
  const int WS_EX_TOOLWINDOW = 0x00000080;
  const int WS_EX_APPWINDOW = 0x00040000;

  public static void RemoveFromAltTab(IntPtr Handle)
  {
    //Make it gone frmo the ALT+TAB
    int windowStyle = GetWindowLong(Handle, GWL_EXSTYLE);
    SetWindowLong(Handle, GWL_EXSTYLE, windowStyle | WS_EX_TOOLWINDOW);

  }

  public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
  {
    if (depObj == null) yield return (T)Enumerable.Empty<T>();
    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
    {
      DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
      if (ithChild == null) continue;
      if (ithChild is T t) yield return t;
      foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
    }
  }
}

