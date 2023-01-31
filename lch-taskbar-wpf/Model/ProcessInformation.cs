using System.Drawing;

namespace lch_taskbar_wpf
{
  public class ProcessInformation
  {
    public IntPtr ProcessHwnd { get; set; }
    public Icon? ProcessIcon { get; set; }
    public string ProcessName { get; set; } = "";
  }
}
