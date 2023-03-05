using InTheHand.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace lch_taskbar.TaskbarComponents
{
  public partial class BluetoothControl : System.Windows.Controls.Button, ICustomButton
  {    
    public BluetoothControl()
    {
      InitializeComponent();
      Refresh();
      Click += CustomButton_Click;
    }

    public void CustomButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:devices";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }

    public void Refresh()
    {
      // TODO : Make something to refresh the bluetooth status
      // var hasPairedDevices = new BluetoothClient().PairedDevices.Any();
      // var BluetoothLogo = hasPairedDevices ? "BluetoothOn" : "BluetoothOff";
      var BluetoothLogo = "BluetoothOn";
      BluetoothIcon.Source = new BitmapImage(new Uri($"/Ressources/{BluetoothLogo}.png", UriKind.Relative));
    }
  }
}
