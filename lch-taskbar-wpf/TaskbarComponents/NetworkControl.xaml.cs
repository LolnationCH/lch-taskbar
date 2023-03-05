using System.Net.NetworkInformation;
using System.Windows;
using lch_configuration.ComponentOptions;

namespace lch_taskbar.TaskbarComponents
{
  public partial class NetworkControl : System.Windows.Controls.Button, ICustomButton
  {
    NetworkOptions options = new();
    public NetworkControl(IComponentOptions? Options)
    {
      if (Options is NetworkOptions networkOptions)
        options = networkOptions;

      InitializeComponent();
      Setup();

      Click += CustomButton_Click;
    }

    private void Setup()
    {
      internetIcon.Visibility = options.ShowIcon ? Visibility.Visible : Visibility.Collapsed;

      var interfaceName = NetworkUtils.GetConnectedInterfaceName();
      if (interfaceName == null)
        return;

      if (interfaceName != "Wi-Fi")
      {
        internetLabel.Text = interfaceName;
        internetIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Ressources/ethernet.png", UriKind.Relative));
        return;
      }

      var WIFIConnections = NetworkUtils.GetWifiConnections();
      if (WIFIConnections == null)
        return;

      var WIFIConnection = WIFIConnections.Where(x => x.IsConnected()).FirstOrDefault();
      internetLabel.Text = NetworkUtils.GetNetworkInformationFormated(WIFIConnection, options.TextFormat);
      internetIcon.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Ressources/wifi.png", UriKind.Relative));
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
      Setup();
    }
  }
}
