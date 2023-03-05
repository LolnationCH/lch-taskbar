using System.Management;

namespace lch_taskbar_wpf.Utils
{
  class Win32_Battery
  {
    public UInt16 Availability;
    public UInt32 BatteryRechargeTime;
    public UInt16 BatteryStatus;
    public string Caption;
    public UInt16 Chemistry;
    public UInt32 ConfigManagerErrorCode;
    public bool ConfigManagerUserConfig;
    public string CreationClassName;
    public string Description;
    public UInt32 DesignCapacity;
    public UInt64 DesignVoltage;
    public string DeviceID;
    public bool ErrorCleared;
    public string ErrorDescription;
    public UInt16 EstimatedChargeRemaining;
    public UInt32 EstimatedRunTime;
    public UInt32 ExpectedBatteryLife;
    public UInt32 ExpectedLife;
    public UInt32 FullChargeCapacity;
    public DateTime InstallDate;
    public UInt32 LastErrorCode;
    public UInt32 MaxRechargeTime;
    public string Name;
    public string PNPDeviceID;
    public UInt16[] PowerManagementCapabilities;
    public bool PowerManagementSupported;
    public string SmartBatteryVersion;
    public string Status;
    public UInt16 StatusInfo;
    public string SystemCreationClassName;
    public string SystemName;
    public UInt32 TimeOnBattery;
    public UInt32 TimeToFullCharge;

    public static Win32_Battery[] GetInstances()
    {
      var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
      var collection = searcher.Get();
      var result = new Win32_Battery[collection.Count];
      int i = 0;
      foreach (var item in collection)
      {
        result[i] = new Win32_Battery(item);
        i++;
      }
      return result;
    }
    
    public Win32_Battery(ManagementBaseObject item)
    {
      Availability = (UInt16)item["Availability"];
      BatteryRechargeTime = (UInt32)item["BatteryRechargeTime"];
      BatteryStatus = (UInt16)item["BatteryStatus"];
      Caption = (string)item["Caption"];
      Chemistry = (UInt16)item["Chemistry"];
      ConfigManagerErrorCode = (UInt32)item["ConfigManagerErrorCode"];
      ConfigManagerUserConfig = (bool)item["ConfigManagerUserConfig"];
      CreationClassName = (string)item["CreationClassName"];
      Description = (string)item["Description"];
      DesignCapacity = (UInt32)item["DesignCapacity"];
      DesignVoltage = (UInt16)item["DesignVoltage"];
      DeviceID = (string)item["DeviceID"];
      ErrorCleared = (bool)item["ErrorCleared"];
      ErrorDescription = (string)item["ErrorDescription"];
      EstimatedChargeRemaining = (UInt16)item["EstimatedChargeRemaining"];
      EstimatedRunTime = (UInt32)item["EstimatedRunTime"];
      ExpectedBatteryLife = (UInt32)item["ExpectedBatteryLife"];
      ExpectedLife = (UInt32)item["ExpectedLife"];
      FullChargeCapacity = (UInt32)item["FullChargeCapacity"];
      InstallDate = (DateTime)item["InstallDate"];
      LastErrorCode = (UInt32)item["LastErrorCode"];
      MaxRechargeTime = (UInt32)item["MaxRechargeTime"];
      Name = (string)item["Name"];
      PNPDeviceID = (string)item["PNPDeviceID"];
      PowerManagementCapabilities = (UInt16[])item["PowerManagementCapabilities"];
      PowerManagementSupported = (bool)item["PowerManagementSupported"];
      SmartBatteryVersion = (string)item["SmartBatteryVersion"];
      Status = (string)item["Status"];
      StatusInfo = (UInt16)item["StatusInfo"];
      SystemCreationClassName = (string)item["SystemCreationClassName"];
      SystemName = (string)item["SystemName"];
      TimeOnBattery = (UInt32)item["TimeOnBattery"];
      TimeToFullCharge = (UInt32)item["TimeToFullCharge"];
    }
  };

  public static class BatteryUtils
  {
    public static string? GetBatteryStatus()
    {
      var batteries = Win32_Battery.GetInstances();
      if (batteries.Length == 0)
      {
        return null;
      }
      return batteries[0].Status;
    }
  }
}
