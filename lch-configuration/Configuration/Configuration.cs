namespace lch_taskbar_wpf.Configuration
{
  public class Configuration
  {
    private static Configuration? _instance;
    private readonly string configPath = "config.json";
    private ConfigurationData configurationData { get; set; } = new();
    public ConfigurationData GetData { get => configurationData; }

    public static Configuration GetInstance()
    {
      if (_instance == null)
      {
        _instance = new Configuration();
      }
      return _instance;
    }
    
    public void Save()
    {
      var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(configurationData);
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
          this.configurationData = configurationData;
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
