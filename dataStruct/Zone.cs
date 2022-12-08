public class Zone
{
  public int x { get; set; }
  public int y { get; set; }
  public int width { get; set; }
  public int height { get; set; }
  public ProgramWindowData? window { get; set; }

  private void Expand(int x, int y, int width, int height)
  {
    this.x += x;
    this.y += y;
    this.width += width;
    this.height += height;
    ShowWindowInZone();
  }

  public override string ToString()
  {
    return $"{x} - {y} - {width} - {height}";
  }

  public void ShowWindowInZone()
  {
    if (window == null)
      return;

    window.GetExecutable();
    window.ResetPosition();
    window.SetPosition(x, y, width, height);
  }

  public void ExpandLeft(int amount)
  {
    Expand(amount, 0, 0, 0);
  }

  public void ExpandRight(int amount)
  {
    Expand(0, 0, amount, 0);
  }

  public void ExpandUp(int amount)
  {
    Expand(0, amount, 0, 0);
  }

  public void ExpandDown(int amount)
  {
    Expand(0, 0, 0, amount);
  }
}