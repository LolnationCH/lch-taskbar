using System.Net.NetworkInformation;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for NetworkControl.xaml
  /// </summary>
  public partial class NetworkControl : System.Windows.Controls.Button
  {
    public NetworkControl()
    {
      InitializeComponent();
      Setup();
    }

    private void Setup()
    {
      var interfaceName = NetworkUtils.GetConnectedInterfaceName();
      if (interfaceName == null)
        return;
      
      if (interfaceName == "Wi-Fi")
      {
        var wifiConnections = NetworkUtils.GetWifiConnections();
        if (wifiConnections == null)
          return;

        var wifiConnection = wifiConnections.Where(x => x.IsConnected()).FirstOrDefault();
        var ssid = wifiConnection.SSID;
        if (string.IsNullOrEmpty(ssid))
          return;

        var signal = wifiConnection.Signal;
        if (string.IsNullOrEmpty(signal))
          return;

        internetLabel.Content = $"{ssid} ({signal})";
        return;
      }

      internetLabel.Content = interfaceName;
      internetIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/TaskbarComponents/ethernet.png", UriKind.Relative));

    }

    private void NetworkPanel_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:network";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
  }    
}
