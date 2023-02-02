using lch_configuration.ComponentOptions;
using lch_configuration.Configuration;
using lch_taskbar.Utils;
using lch_taskbar_wpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.Design.AxImporter;

namespace lch_taskbar_wpf.Windows
{
  public partial class Settings : Window
  {
    public static readonly RoutedCommand SaveCommand = new();
    private readonly List<string> FontSizeSource = FontUtils.GetFontSize();
    private readonly List<string> FontFamilySource = FontUtils.GetFontFamilies();
    private TabItem? ComponentTabSelected;
    
    private static readonly Dictionary<string, IComponentOptions?> componentsOptionDic = ComponentFactory.GetOptionsByComponentName();
    private static readonly List<string> componentsName = new(componentsOptionDic.Keys);

    public Settings()
    {
      InitializeComponent();
      SetupComboBoxSource();
      DataContext = Configuration.GetInstance().GetData;
      SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
    }

    private void SetupComboBoxSource()
    {
      FontSizeComboBox.ItemsSource = FontSizeSource;      
      FontFamilyComboBox.ItemsSource = FontFamilySource;
      ClrPcker_Background.SelectedColor = (Color)ColorConverter.ConvertFromString(Configuration.GetInstance().GetData.BackgroundColor);
      ClrPcker_FontColor.SelectedColor = (Color)ColorConverter.ConvertFromString(Configuration.GetInstance().GetData.FontColor);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      if (DataContext is ConfigurationData newConfig)
      {
        Configuration.GetInstance().SetData(newConfig);
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
      Save_Click(sender, e);
    }

    private void ComponentTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
      ComponentTabSelected = (sender as TabControl)!.SelectedItem! as TabItem;
    }

    private void AddComponent_Click(object sender, RoutedEventArgs e)
    {
      var sp = ComponentTabSelected?.Content;
      if (sp is StackPanel)
      {
        var newSp = new StackPanel(){
          Orientation = Orientation.Horizontal,
          Margin = new Thickness(0, 0, 0, 5)
        };
        var newComponent = new ComboBox
        {
          ItemsSource = componentsName,
          MinWidth = 200,
        };
        newComponent.SelectionChanged += Component_SelectionChanged;
        newSp.Children.Add(newComponent);
        (sp as StackPanel)!.Children.Add(newSp);
      }
    }

    private void Component_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var selection = (sender as ComboBox)!.SelectedItem.ToString();
      if (selection != null && componentsOptionDic.ContainsKey(selection))
      {
        var options = componentsOptionDic[selection];
        if (options == null)
          return;

        var sp = (sender as ComboBox)!.Parent! as StackPanel;
        var editButton = new Button()
        {
          Content = "Edit",
          Margin = new Thickness(5, 0, 0, 0),
          Tag = options,
        };
        editButton.Click += EditButton_Click;
        sp!.Children.Add(editButton);
      }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
      var options = (sender as Button)!.Tag as IComponentOptions;
      if (options == null)
        return;
      
      var type = options.GetType();
      var properties = type.GetProperties();

      // TODO : Make a window by type of options
    }
  }
}
