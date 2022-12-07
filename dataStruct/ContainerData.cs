public class ContainerData
{
  public string Title { get; set; } = string.Empty;
  private List<ProgramWindowData> _windows = new();

  public int x;
  public int y;
  public int width;
  public int height;

  private List<Zone> _zones = new();

  public void AddWindow(ProgramWindowData window)
  {
    _windows.Add(window);
  }

  public void OrganizeWindows()
  {
    _zones = ZoneCalculator.CalculateZones(this.x, this.y, this.width, this.height, _windows.Count);
    Console.WriteLine(String.Join(Environment.NewLine, _zones));

    for (var i = 0; i < _windows.Count; i++)
    {
      _windows[i].ResetPosition();
      _windows[i].SetPosition(_zones[i].x,
                                     _zones[i].y,
                                 _zones[i].width,
                                _zones[i].height);
    }
  }
}