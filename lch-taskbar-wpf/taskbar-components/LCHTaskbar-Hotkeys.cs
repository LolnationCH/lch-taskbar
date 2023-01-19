using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace lch_taskbar_wpf
{
  public partial class LCHTaskbar : System.Windows.Window
  {
    private void Toggle()
    {
      if (this.IsVisible)
        this.Hide();
      else
        this.Show();
      WindowsTaskbar.Toggle();
    }
    protected override void OnClosing(CancelEventArgs e)
    {
      WindowsTaskbar.Show();
      base.OnClosing(e);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
      base.OnSourceInitialized(e);

      IntPtr handle = new WindowInteropHelper(this).Handle;
      source = HwndSource.FromHwnd(handle);
      source.AddHook(HwndHook);

      RegisterHotkeys(handle);
    }

    private void RegisterHotkeys(IntPtr handle)
    {
      GlobalHotkeys.RegisterHotKey(handle, GlobalHotkeys.HOTKEY_ID, GlobalHotkeys.MOD_HYPER, (uint)GlobalHotkeys.VK.KEY_H);

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

    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      const int WM_HOTKEY = 0x0312;
      switch (msg)
      {
        case WM_HOTKEY:
          switch (wParam.ToInt32())
          {
            case GlobalHotkeys.HOTKEY_ID:
              int vkey = (((int)lParam >> 16) & 0xFFFF);
              if (vkey == (uint)GlobalHotkeys.VK.KEY_H)
              {
                Toggle();
              }
              else if (vkey >= (uint)GlobalHotkeys.VK.KEY_0 && vkey <= (uint)GlobalHotkeys.VK.KEY_9)
              {
                LaunchProcessByIndex((int)(vkey - (uint)GlobalHotkeys.VK.KEY_0) - 1);
              }
              handled = true;
              break;
          }
          break;
      }
      return IntPtr.Zero;
    }
  }
}
