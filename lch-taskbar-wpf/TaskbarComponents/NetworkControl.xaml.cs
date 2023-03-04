using System.Net.NetworkInformation;
using System.Windows;

namespace lch_taskbar.TaskbarComponents
{
  public partial class NetworkControl : System.Windows.Controls.Button, ICustomButton
  {
    public NetworkControl()
    {
      InitializeComponent();
      Setup();
      
      Click += CustomButton_Click;
    }

    private void Setup()
    {
      var interfaceName = NetworkUtils.GetConnectedInterfaceName();
      if (interfaceName == null)
        return;
      
      if (interfaceName == "Wi-Fi")
      {
        var WIFIConnections = NetworkUtils.GetWifiConnections();
        if (WIFIConnections == null)
          return;

        var WIFIConnection = WIFIConnections.Where(x => x.IsConnected()).FirstOrDefault();
        var SSID = WIFIConnection.SSID;
        if (string.IsNullOrEmpty(SSID))
          return;

        var signal = WIFIConnection.Signal;
        if (string.IsNullOrEmpty(signal))
          return;

        internetLabel.Text = $"{SSID} ({signal})";
        return;
      }

      internetLabel.Text = interfaceName;
      internetIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Ressources/ethernet.png", UriKind.Relative));
      
    }
    public void CustomButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:network";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }

    public void Refresh()
    {
    }
  }    
}
