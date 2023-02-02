using lch_taskbar.TaskbarComponents;
using System.Windows.Interop;

namespace lch_taskbar
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    protected override void OnSourceInitialized(EventArgs e)
    {
      base.OnSourceInitialized(e);

      IntPtr handle = new WindowInteropHelper(this).Handle;
      source = HwndSource.FromHwnd(handle);
      source.AddHook(HwndHook);

      RegisterHotkeys(handle);

      WindowUtils.RemoveFromAltTab(handle);
    }

    private static void RegisterHotkeys(IntPtr handle)
    {
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_H);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_R);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_G);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_C);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_I);

      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_1);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_2);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_3);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_4);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_5);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_6);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_7);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_8);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_9);
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_0);
    }

    private void HandleHookPress(int vkey, ref bool handled)
    {
      switch (vkey)
      {
        case (int)GlobalHotkeys.VK.KEY_H:
          Toggle();
          break;
        case (int)GlobalHotkeys.VK.KEY_R:
          Reload();
          break;
        case (int)GlobalHotkeys.VK.KEY_G:
          ToggleOnlyThis();
          break;
        case (int)GlobalHotkeys.VK.KEY_C:
          Close();
          break;
        case (int)GlobalHotkeys.VK.KEY_I:
          var settings = new lch_taskbar_wpf.Windows.Settings();
          settings.ShowDialog();
          Reload();
          break;
        case (int)GlobalHotkeys.VK.KEY_1:
        case (int)GlobalHotkeys.VK.KEY_2:
        case (int)GlobalHotkeys.VK.KEY_3:
        case (int)GlobalHotkeys.VK.KEY_4:
        case (int)GlobalHotkeys.VK.KEY_5:
        case (int)GlobalHotkeys.VK.KEY_6:
        case (int)GlobalHotkeys.VK.KEY_7:
        case (int)GlobalHotkeys.VK.KEY_8:
        case (int)GlobalHotkeys.VK.KEY_9:
        case (int)GlobalHotkeys.VK.KEY_0:
          var control = GetProcessControls().FirstOrDefault();
          if (control == null)
            break;
          
          control.LaunchProcessByIndex((int)(vkey - (uint)GlobalHotkeys.VK.KEY_0) - 1);
          break;
      }
      handled = true;
    }

    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      const int WM_HOTKEY = 0x0312;
      if (msg != WM_HOTKEY)
        return IntPtr.Zero;

      if (wParam.ToInt32() == GlobalHotkeys.HOTKEY_ID)
      {
        checked
        {
          int vkey = ((int)lParam >> 16) & 0xFFFF;
          HandleHookPress(vkey, ref handled);
        }
      }

      return IntPtr.Zero;
    }
  }
}
