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

    window.ResetPosition();
    window.SetPosition(x, y, width, height);
  }

  public void MoveLeft(int amount)
  {
    Expand(amount * -1, 0, 0, 0);
  }

  public void MoveRight(int amount)
  {
    Expand(amount, 0, 0, 0);
  }

  public void MoveUp(int amount)
  {
    Expand(0, amount * -1, 0, 0);
  }

  public void MoveDown(int amount)
  {
    Expand(0, amount, 0, 0);
  }
}