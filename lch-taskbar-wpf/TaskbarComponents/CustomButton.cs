namespace lch_taskbar.TaskbarComponents
{
  public interface ICustomButton
  {
    public void Refresh();

    public void CustomButton_Click(object sender, System.Windows.RoutedEventArgs e);
  }
}
