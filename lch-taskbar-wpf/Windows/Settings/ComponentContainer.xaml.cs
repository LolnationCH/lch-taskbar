using lch_configuration.Configuration;
using System.Windows.Controls;

namespace lch_taskbar_wpf.Windows.Settings
{
  public partial class ComponentContainer : StackPanel
  {
    public ComponentContainer()
    {
      InitializeComponent();
    }

    public List<Component> GetComponents()
    {
      var components = new List<Component>();
      foreach (var componentLine in Children)
      {
        if (componentLine is ComponentLine line)
        {
          var component = line.GetComponent();
          if (component != null)
            components.Add(component);
        }
      }
      return components;
    }

    public void SetComponents(List<Component> components)
    {
      foreach (var component in components)
      {
        var componentLine = new ComponentLine(this);
        componentLine.SetComponentInformation(component);
        Children.Add(componentLine);
      }
    }

    public void AddComponentLine()
    {
      var componentLine = new ComponentLine(this);
      Children.Add(componentLine);
    }

    public void RemoveComponentLine(ComponentLine componentLine)
    {
      Children.Remove(componentLine);
    }

    public void MoveUpComponentLine(ComponentLine componentLine)
    {
      var index = Children.IndexOf(componentLine);
      if (index > 0)
      {
        Children.Remove(componentLine);
        Children.Insert(index - 1, componentLine);
      }
    }

    public void MoveDownComponentLine(ComponentLine componentLine)
    {
      var index = Children.IndexOf(componentLine);
      if (index < Children.Count - 1)
      {
        Children.Remove(componentLine);
        Children.Insert(index + 1, componentLine);
      }
    }
  }
}
