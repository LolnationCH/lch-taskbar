using Newtonsoft.Json;

namespace lch_configuration.Configuration
{
  public class Configuration
  {
    private readonly JsonSerializerSettings settings = new() { TypeNameHandling = TypeNameHandling.All };
    private static Configuration? _instance;
    private readonly string configPath = "config.json";
    private ConfigurationData ConfigurationData { get; set; } = new();
    public ConfigurationData GetData { get => ConfigurationData; }
    public void SetData(ConfigurationData data) => ConfigurationData = data;

    public static Configuration GetInstance()
    {
      _instance ??= new Configuration();
      return _instance;
    }

    private void SetAsDefault()
    {
      ConfigurationData.ComponentList.SetAsDefault();
    }
    
    public void Save()
    {
      SetAsDefault();
      var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ConfigurationData, settings);
      System.IO.File.WriteAllText(configPath, jsonString);
    }
    
    public void Load()
    {
      if (System.IO.File.Exists(configPath))
      {
        string json = System.IO.File.ReadAllText(configPath);
        var configurationData = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationData>(json, settings);
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
