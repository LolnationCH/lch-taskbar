using lch_configuration.ComponentOptions;

namespace lch_configuration.Configuration
{
  public static class ComponentFactory
  {
    static readonly Dictionary<string, IComponentOptions?>  optionsByComponentName = new()
      {
        { "input", new InputOptions()},
        { "bluetooth", null},
        { "network", null},
        { "process", null},
        { "processes", null},
        { "volume", new SoundOptions()},
        { "sound", new SoundOptions()},
        { "shortcuts" , new ShortcutOptions()},
        { "spotify", null},
        { "time", new TimeOptions()},
        { "date", new TimeOptions()},
        { "weather", new WeatherOptions()},
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
