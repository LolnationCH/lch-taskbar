using InTheHand.Net.Sockets;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace lch_taskbar_wpf.TaskbarComponents
{
  /// <summary>
  /// Interaction logic for BluetoothControl.xaml
  /// </summary>
  public partial class BluetoothControl : System.Windows.Controls.Button
  {    
    public BluetoothControl()
    {
      InitializeComponent();
      Refresh();

      DispatcherTimer LiveTime = new DispatcherTimer();
      LiveTime.Interval = TimeSpan.FromSeconds(1);
      LiveTime.Tick += RefreshBluetoothLogo_tick; ;
      LiveTime.Start();
    }

    private void RefreshBluetoothLogo_tick(object? sender, EventArgs e)
    {
      Refresh();
    }

    private void BluetoohOpenMenu_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      var process = new System.Diagnostics.Process();
      process.StartInfo.FileName = "ms-settings:devices";
      process.StartInfo.UseShellExecute = true;
      process.Start();
    }
    
    private void Refresh()
    {
      // Get the Bluetooth devices
      var hasPairedDevices = new BluetoothClient().PairedDevices.Count() != 0;
      if (hasPairedDevices)
      {
        BluetoothIcon.Source = new BitmapImage(new Uri("/TaskbarComponents/BluetoothOn.png", UriKind.Relative));
      }
      else
      {
        BluetoothIcon.Source = new BitmapImage(new Uri("/TaskbarComponents/BluetoothOff.png", UriKind.Relative));
      }
    }
  }
}
