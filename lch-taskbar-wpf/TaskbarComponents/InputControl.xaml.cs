using lch_configuration.ComponentOptions;
using lch_taskbar.TaskbarComponents;
using lch_taskbar_wpf.Utils;
using System.Windows.Controls;

namespace lch_taskbar_wpf.TaskbarComponents
{
  public partial class InputControl : StackPanel
  {
    InputOptions options = new();
    public InputControl(IComponentOptions? Options)
    {
      if (Options is InputOptions inputOptions)
        options = inputOptions;

      InitializeComponent();
      Orientation = ControlsUtils.GetOrientationBasedOnConfig();
      SetupControlBasedOnOptions();
    }

    private void SetupControlBasedOnOptions()
    {
      if (options.Icon != null)
      {
        var icon = ControlsUtils.GetImageFromImagePath(options.Icon, options.Tooltip);
        icon.Margin = new System.Windows.Thickness(0, 3, 3, 3);
        Children.Add(icon);
      }

      var textBox = new TextBox()
      {
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
        VerticalAlignment = System.Windows.VerticalAlignment.Center,
        MinWidth = options.MinWidth,
        Margin = new System.Windows.Thickness(0, 3, 0, 3),
      };
      textBox.KeyDown += InputTextBox_KeyDown;
      Children.Add(textBox);
    }

    private string PrepareCommandOptions(string input)
    {
      if (options.CommandOptions != null &&
          options.CommandOptions.Contains("{0}"))
        return string.Format(options.CommandOptions, input);

      return options.CommandOptions + " " + input;
    }

    private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.Enter)
      {
        var textBox = (TextBox)sender;
        var newProcess = new System.Diagnostics.Process();
        newProcess.StartInfo.FileName = options.CommandString;
        newProcess.StartInfo.Arguments = PrepareCommandOptions(textBox.Text);
        newProcess.StartInfo.UseShellExecute = true;
        newProcess.Start();
      }
    }
  }
}
