using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lch_configuration.CustomTypes
{
  public class ProcessInformation
  {
    public string? Name { get; set; }
    public string? Path { get; set; }
  }

  public class ProcessList
  {
    public List<ProcessInformation> ProcessNamesExcluded { get; set; } = new();

    public void AddChildren(ProcessInformation process) { ProcessNamesExcluded.Add(process); }
    
    public void RemoveChildren(ProcessInformation process) {  ProcessNamesExcluded.Remove(process); }

    public void RemoveAllEmpty() { ProcessNamesExcluded.RemoveAll(x => x.Name == null); }

    public bool ContainsProcess(string processName, string? path) { return ProcessNamesExcluded.Any(x => x.Name == processName || x.Path == path); }

    public IEnumerator<ProcessInformation> GetEnumerator()
    {
      return ProcessNamesExcluded.GetEnumerator();
    }
  }
}
