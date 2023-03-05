using lch_configuration.ComponentOptions;
using lch_taskbar.TaskbarComponents;
using lch_taskbar_wpf.Utils;
using System.Windows.Controls;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class InputControl : StackPanel
  {
    InputOptions inputOptions = new();
    public InputControl(IComponentOptions? componentOptions)
    {
      if (componentOptions != null)
        inputOptions = (InputOptions)componentOptions;

      InitializeComponent();
      Orientation = ControlsUtils.GetOrientationBasedOnConfig();
      SetupControlBasedOnOptions();
    }

    private void SetupControlBasedOnOptions()
    {
      if (inputOptions.Icon != null)
      {
        var icon = ControlsUtils.GetImageFromImagePath(inputOptions.Icon, inputOptions.Tooltip);
        icon.Margin = new System.Windows.Thickness(0, 3, 3, 3);
        Children.Add(icon);
      }

      var textBox = new TextBox()
      {
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
        VerticalAlignment = System.Windows.VerticalAlignment.Center,
        MinWidth = inputOptions.MinWidth,
        Margin = new System.Windows.Thickness(0, 3, 0, 3),
      };
      textBox.KeyDown += InputTextBox_KeyDown;
      Children.Add(textBox);
    }

    private string PrepareCommandOptions(string input)
    {
      if (inputOptions.CommandOptions != null && 
          inputOptions.CommandOptions.Contains("{0}"))
        return string.Format(inputOptions.CommandOptions, input);
      
      return inputOptions.CommandOptions + " " + input;
    }

    private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.Enter)
      {
        var textBox = (TextBox)sender;
        var newProcess = new System.Diagnostics.Process();
        newProcess.StartInfo.FileName = inputOptions.CommandString;
        newProcess.StartInfo.Arguments = PrepareCommandOptions(textBox.Text);
        newProcess.StartInfo.UseShellExecute = true;
        newProcess.Start();
      }
    }
  }
}
