using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public struct WifiConnection
{
  public string Name;
  public string Description;
  public string GUID;
  public string Physical_address;
  public string Interface_type;
  public string State;
  public string SSID;
  public string BSSID;
  public string Network_type;
  public string Radio_type;
  public string Authentication;
  public string Cipher;
  public string Connection_mode;
  public string Band;
  public string Channel;
  public string Receive_rate;
  public string Transmit_rate;
  public string Signal;
  public string Profile;

  public bool IsConnected()
  {
    return State == "connected";
  }
}

public static class NetworkUtils
{
  const int linePerInterface = 20;

  public static List<WifiConnection> GetWifiConnections()
  {
    Process process = new();
    process.StartInfo.FileName = "netsh.exe";
    process.StartInfo.Arguments = "wlan show interface";
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardInput = true;
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    string output = process.StandardOutput.ReadToEnd();
    process.WaitForExit();

    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                      .Where(x => !string.IsNullOrEmpty(x)).ToList();

    // Regex to check how many interface found
    Regex regex = new Regex(@"There is (\d+) interface on the system:");
    Match match = regex.Match(lines.First());
    int interfaceCount = int.Parse(match.Groups[1].Value);
    lines = lines.Skip(1).ToList();

    List<WifiConnection> wifiConnections = new();
    for (var i = 0; i < interfaceCount; i++)
    {
      WifiConnection wifiConnection = new();
      wifiConnection.Name =               lines[i * linePerInterface + 0].Split(':')[1].Trim();
      wifiConnection.Description =        lines[i * linePerInterface + 1].Split(':')[1].Trim();
      wifiConnection.GUID =               lines[i * linePerInterface + 2].Split(':')[1].Trim();
      wifiConnection.Physical_address =   lines[i * linePerInterface + 3].Split(':')[1].Trim();
      wifiConnection.Interface_type =     lines[i * linePerInterface + 4].Split(':')[1].Trim();
      wifiConnection.State =              lines[i * linePerInterface + 5].Split(':')[1].Trim();
      wifiConnection.SSID =               lines[i * linePerInterface + 6].Split(':')[1].Trim();
      wifiConnection.BSSID =              lines[i * linePerInterface + 7].Split(':')[1].Trim();
      wifiConnection.Network_type =       lines[i * linePerInterface + 8].Split(':')[1].Trim();
      wifiConnection.Radio_type =         lines[i * linePerInterface + 9].Split(':')[1].Trim();
      wifiConnection.Authentication =     lines[i * linePerInterface + 10].Split(':')[1].Trim();
      wifiConnection.Cipher =             lines[i * linePerInterface + 11].Split(':')[1].Trim();
      wifiConnection.Connection_mode =    lines[i * linePerInterface + 12].Split(':')[1].Trim();
      wifiConnection.Band =               lines[i * linePerInterface + 13].Split(':')[1].Trim();
      wifiConnection.Channel =            lines[i * linePerInterface + 14].Split(':')[1].Trim();
      wifiConnection.Receive_rate =       lines[i * linePerInterface + 15].Split(':')[1].Trim();
      wifiConnection.Transmit_rate =      lines[i * linePerInterface + 16].Split(':')[1].Trim();
      wifiConnection.Signal =             lines[i * linePerInterface + 17].Split(':')[1].Trim();
      wifiConnection.Profile =            lines[i * linePerInterface + 18].Split(':')[1].Trim();
      wifiConnections.Add(wifiConnection);
    }

    Console.WriteLine(output);
    return wifiConnections;
  }

  public static string? GetConnectedInterfaceName()
  {
    Process process = new();
    process.StartInfo.FileName = "netsh.exe";
    process.StartInfo.Arguments = "interface show interface";
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardInput = true;
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    string output = process.StandardOutput.ReadToEnd();
    process.WaitForExit();

    var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                      .Where(x => !string.IsNullOrEmpty(x)).Skip(2).ToList();

    List<List<string>> interfaces = new();
    foreach(var line in lines)
      interfaces.Add(line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList());

    var connectedInterface = interfaces.FirstOrDefault(x => x[1] == "Connected");
    if (connectedInterface == null)
      return null;

    return string.Join(" ", connectedInterface.Skip(3));
  }
}
