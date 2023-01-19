using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lch_taskbar_wpf
{
  public class ProcessInformation
  {
    public IntPtr ProcessHwnd { get; set; }
    public Icon? ProcessIcon { get; set; }
    public string ProcessName { get; set; } = "";
  }
}
