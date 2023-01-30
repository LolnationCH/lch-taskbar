namespace lch_taskbar_wpf.Configuration
{
  public class Configuration
  {
    private static Configuration? _instance;
    private readonly string configPath = "config.json";
    private ConfigurationData ConfigurationData { get; set; } = new();
    public ConfigurationData GetData { get => ConfigurationData; }

    public static Configuration GetInstance()
    {
      _instance ??= new Configuration();
      return _instance;
    }
    
    public void Save()
    {
      var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ConfigurationData);
      System.IO.File.WriteAllText(configPath, jsonString);
    }
    
    public void Load()
    {
      if (System.IO.File.Exists(configPath))
      {
        string json = System.IO.File.ReadAllText(configPath);
        var configurationData = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationData>(json);
        if (configurationData != null)
        {
          this.ConfigurationData = configurationData;
        }
      }
      else
      {
        Save();
      }
    }

    public void Reload()
    {
      Load();
    }
    
    public Configuration()
    {
      Load();
    }
  }
}
