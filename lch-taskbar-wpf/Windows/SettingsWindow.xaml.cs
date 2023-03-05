using lch_configuration.Configuration;
using lch_taskbar.Utils;
using lch_taskbar_wpf.Utils;
using lch_taskbar_wpf.Windows.Settings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lch_taskbar_wpf.Windows
{
  public partial class SettingsWindow : Window
  {
    public static readonly RoutedCommand SaveCommand = new();
    private readonly List<string> FontSizeSource = FontUtils.GetFontSize();
    private readonly List<string> FontFamilySource = FontUtils.GetFontFamilies();
    private TabItem? ComponentTabSelected;
    
    private static readonly List<string> componentsName = new(ComponentFactory.GetOptionsByComponentName().Keys);

    public SettingsWindow()
    {
      InitializeComponent();
      SetupComboBoxSource();
      DataContext = Configuration.GetInstance().GetData;
      SetComponents();
      SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
    }

    private void SetupComboBoxSource()
    {
      FontSizeComboBox.ItemsSource = FontSizeSource;      
      FontFamilyComboBox.ItemsSource = FontFamilySource;
      ClrPcker_Background.SelectedColor = (Color)ColorConverter.ConvertFromString(Configuration.GetInstance().GetData.BackgroundColor);
      ClrPcker_FontColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Configuration.GetInstance().GetData.FontColor);
    }

    private Components GetComponents()
    {
      return new()
      {
        LeftComponents = LeftSide.GetComponents(),
        MiddleComponents = MiddleSide.GetComponents(),
        RightComponents = RightSide.GetComponents(),
      };
    }

    private void SetComponents()
    {
      if (DataContext is ConfigurationData newConfig)
      {
        LeftSide.SetComponents(newConfig.ComponentList.LeftComponents);
        MiddleSide.SetComponents(newConfig.ComponentList.MiddleComponents);
        RightSide.SetComponents(newConfig.ComponentList.RightComponents);
      }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      if (DataContext is ConfigurationData newConfig)
      {
        Configuration.GetInstance().SetData(newConfig);
        Configuration.GetInstance().SetComponents(GetComponents());
        Configuration.GetInstance().Save();
      }
      Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
    {
      if (DataContext is ConfigurationData newConfig)
        newConfig.BackgroundColor = ColorUtils.GetHexColor(ClrPcker_Background.SelectedColor);
    }
    private void ClrPcker_FontColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
    {
      if (DataContext is ConfigurationData newConfig)
        newConfig.FontColor = ColorUtils.GetHexColor(ClrPcker_FontColor.SelectedColor);
    }

    private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      object focusObj = FocusManager.GetFocusedElement(FocusManager.GetFocusScope(this));
      if (focusObj != null && focusObj is TextBox)
      {
        var binding = ((TextBox)focusObj).GetBindingExpression(TextBox.TextProperty);
        binding.UpdateSource();
      }


      Save_Click(sender, e);
    }

    private void ComponentTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
      ComponentTabSelected = (sender as TabControl)!.SelectedItem! as TabItem;
    }

    private void AddComponent_Click(object sender, RoutedEventArgs e)
    {
      var componentContainer = ComponentTabSelected?.Content;
      if (componentContainer is ComponentContainer)
        (componentContainer as ComponentContainer)!.AddComponentLine();
    }
  }
}
