using lch_configuration.ComponentOptions;

namespace lch_configuration.Configuration
{
  public static class ComponentFactory
  {
    static readonly Dictionary<string, IComponentOptions?>  optionsByComponentName = new()
      {
        {"bluetooth", null},
        {"everything", null},
        {"network", null},
        {"process", null},
        {"processes", null},
        {"volume", null},
        {"sound", null},
        {"shortcuts" , new ShortcutDatas()},
        {"spotify", null},
        { "time", null},
        { "date", null},
        { "weather", null},
        { "title", null},
      };
    
  public static Dictionary<string, IComponentOptions?> GetOptionsByComponentName()
    {
      return optionsByComponentName;
    }

    public static Component? CreateComponentFromName(string? name)
    {
      if (name == null)
        return null;
      
      if (optionsByComponentName.ContainsKey(name))
        return new Component() { Name = name, Options = optionsByComponentName[name] };
      
      return null;
    }

    public static Component? CreateComponentFromNameAndOptions(string? name, IComponentOptions? options)
    {
      return new Component() { Name = name, Options = options };
    }
  }
}
