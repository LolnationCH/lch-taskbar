public class ContainerData
{
  public string Title { get; set; } = string.Empty;
  private List<ProgramWindowData> _windows = new();

  public int x;
  public int y;
  public int width;
  public int height;

  public bool isActive = false;

  private List<Zone> _zones = new();

  public void AddWindow(ProgramWindowData window)
  {
    _windows.Add(window);
  }

  public void RemoveWindow(ProgramWindowData window)
  {
    _windows.Remove(window);
  }

  public void ShowContainer()
  {
    isActive = true;
    Console.WriteLine($"Container: {Title} - {x} - {y} - {width} - {height}");
    RenderContainer();
  }

  public void RenderContainer()
  {
    if (_zones.Count == 0 || _zones.Count != _windows.Count)
      _zones = ZoneCalculator.CalculateZones(this.x, this.y, this.width, this.height, _windows.Count).ToList();

    Console.WriteLine(String.Join(Environment.NewLine, _zones));

    for (var i = 0; i < _windows.Count; i++)
    {
      var zone = _zones[i];
      zone.window = _windows[i];
      _zones[i].ShowWindowInZone();
    }
  }
}