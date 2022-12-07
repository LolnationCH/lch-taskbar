public struct Zone
{
  public int x { get; set; }
  public int y { get; set; }
  public int width { get; set; }
  public int height { get; set; }

  public override string ToString()
  {
    return $"{x} - {y} - {width} - {height}";
  }
}

public class ZoneCalculator
{
  public static List<Zone> CalculateZones(int x, int y, int width, int height, int zoneCount)
  {
    if (zoneCount == 1)
      return new List<Zone>() { new Zone() { x = x, y = y, width = width, height = height } };

    bool isPortraitOrientation = (width / height) < 1;
    bool hasTwoZone = zoneCount == 2;

    List<Zone> zones = new();
    int zoneWidth = width / zoneCount;
    if (isPortraitOrientation && hasTwoZone)
      zoneWidth = width;

    int zoneHeight = height / zoneCount;
    if (!isPortraitOrientation && hasTwoZone)
      zoneHeight = height;

    for (int i = 0; i < zoneCount; i++)
    {
      (var incrementX, var incrementY) = CalculateIncrement(isPortraitOrientation, i);
      zones.Add(new Zone()
      {
        x = x + (i * zoneWidth * incrementX),
        y = y + (i * zoneHeight * incrementY),
        width = zoneWidth,
        height = zoneHeight
      });
    }
    return zones;
  }

  private static Tuple<int, int> CalculateIncrement(bool isPortraitOrientation, int index)
  {
    int incrementX = index % 2 != 0 ? 1 : 0;
    int incrementY = index % 2 == 0 ? 1 : 0;
    if (isPortraitOrientation)
    {
      incrementX = index % 2 == 0 ? 1 : 0;
      incrementY = index % 2 != 0 ? 1 : 0;
    }
    return new Tuple<int, int>(incrementX, incrementY);
  }
}