using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

class InterceptKeys
{
  private const int WH_KEYBOARD_LL = 13;
  private const int WM_KEYDOWN = 0x0100;
  private const int WM_KEYUP = 0x0101;
  private const int WM_SYSKEYDOWN = 0x0104;
  private const int WM_SYSKEYUP = 0x0105;
  private static LowLevelKeyboardProc _proc = HookCallback;
  private static IntPtr _hookID = IntPtr.Zero;

  private static Dictionary<Keys, bool> _keysDown = new();

  public static void InitHook()
  {
    Enum.GetValues(typeof(Keys))
        .Cast<Keys>()
        .ToList()
        .ForEach(x => _keysDown[x] = false);

    _hookID = SetHook(_proc);
    Application.Run();
    UnhookWindowsHookEx(_hookID);
  }

  private static IntPtr SetHook(LowLevelKeyboardProc proc)
  {
    using (Process curProcess = Process.GetCurrentProcess())
    using (ProcessModule curModule = curProcess.MainModule!)
    {
      return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
          GetModuleHandle(curModule!.ModuleName), 0);
    }
  }

  private delegate IntPtr LowLevelKeyboardProc(
      int nCode, IntPtr wParam, IntPtr lParam);

  private static IntPtr HookCallback(
      int nCode, IntPtr wParam, IntPtr lParam)
  {
    if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
    {
      int vkCode = Marshal.ReadInt32(lParam);
      _keysDown[(Keys)vkCode] = true;
    }
    else if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
    {
      int vkCode = Marshal.ReadInt32(lParam);
      _keysDown[(Keys)vkCode] = false;
    }

    if (LogicHandler.HandleKeyCombination(_keysDown))
      return (IntPtr)1;

    return CallNextHookEx(_hookID, nCode, wParam, lParam);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr SetWindowsHookEx(int idHook,
      LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool UnhookWindowsHookEx(IntPtr hhk);

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
      IntPtr wParam, IntPtr lParam);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr GetModuleHandle(string lpModuleName);
}