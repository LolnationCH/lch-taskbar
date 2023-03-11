using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lch_configuration.CustomTypes
{
  public class ProcessInformation
  {
    public required string Name { get; set; }
    public required string Path { get; set; }
    public required string Icon { get; set; }
  }

  public class ProcessList
  {
    public List<ProcessInformation> ProcessNamesExcluded { get; set; } = new();
  }
}
