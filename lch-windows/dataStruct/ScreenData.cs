public class ScreenData
{
  public string DeviceName { get; set; } = string.Empty;
  public Rectangle Bounds { get; set; }
  public Rectangle WorkingArea { get; set; }
  public bool Primary { get; set; }

  public override string ToString()
  {
    return $"{DeviceName} - {Bounds} - {WorkingArea} - {Primary}";
  }

  public int TaskbarHeight { get; set; } = 40;
  public enum TaskbarPosition
  {
    Unknown = 0,
    Left = 1,
    Top = 2,
    Right = 3,
    Bottom = 4,
    Hidden = 5
  }
  public TaskbarPosition TaskbarPositionEnum { get; set; } = TaskbarPosition.Bottom;

  public List<ContainerData> _containers = new();
  private int _currentContainerIndex = 0;

  public void AddWindow(ProgramWindowData window, bool NewContainer = false)
  {
    if (NewContainer || _containers.Count == 0)
    {
      ContainerData container = new()
      {
        x = Bounds.X,
        y = Bounds.Y + (TaskbarPositionEnum == TaskbarPosition.Top ? TaskbarHeight : 0),
        width = Bounds.Width,
        height = Bounds.Height - TaskbarHeight
      };

      container.AddWindow(window);
      _containers.Add(container);
      return;
    }
    else
      _containers[_currentContainerIndex].AddWindow(window);
  }

  public void SwitchContainer(bool next = true)
  {
    if (_containers.Count == 0)
      return;

    if (next)
      _currentContainerIndex++;
    else
      _currentContainerIndex--;
    if (_currentContainerIndex >= _containers.Count)
      _currentContainerIndex = 0;

    _containers[_currentContainerIndex].ShowContainer();
  }

  public void RenderScreen()
  {
    _containers[_currentContainerIndex].RenderContainer();
  }

  public Zone? FindActiveZone(IntPtr handle)
  {
    return _containers[_currentContainerIndex].FindActiveZone(handle);
  }

}