public class ZoneCalculator
{
  public static IEnumerable<Zone> CalculateZones(int x, int y, int width, int height, int zoneCount)
  {
    if (zoneCount == 1)
      return new List<Zone>() { new Zone() { x = x, y = y, width = width, height = height } };

    return IsPortraitOrientation(width, height) ? CalculateZonePortrait(x, y, width, height, zoneCount) :
                                                  CalculateZoneLandscape(x, y, width, height, zoneCount);
  }

  private static IEnumerable<Zone> CalculateZoneLandscape(int x, int y, int width, int height, int zoneCount)
  {
    List<Zone> zones = new();
    switch (zoneCount)
    {
      case 2:
        zones.Add(new Zone() { x = x, y = y, width = width / 2, height = height });
        zones.Add(new Zone() { x = x + width / 2, y = y, width = width / 2, height = height });
        break;
      case 3:
        zones.Add(new Zone() { x = x, y = y, width = width / 2, height = height });
        zones.Add(new Zone() { x = x + width / 2, y = y, width = width / 2, height = height / 2 });
        zones.Add(new Zone() { x = x + width / 2, y = y + height / 2, width = width / 2, height = height / 2 });
        break;
      case 4:
        zones.Add(new Zone() { x = x, y = y, width = width / 2, height = height / 2 });
        zones.Add(new Zone() { x = x + width / 2, y = y, width = width / 2, height = height / 2 });
        zones.Add(new Zone() { x = x, y = y + height / 2, width = width / 2, height = height / 2 });
        zones.Add(new Zone() { x = x + width / 2, y = y + height / 2, width = width / 2, height = height / 2 });
        break;
    }

    return zones;
  }

  private static IEnumerable<Zone> CalculateZonePortrait(int x, int y, int width, int height, int zoneCount)
  {
    List<Zone> zones = new();
    switch (zoneCount)
    {
      case 2:
        zones.Add(new Zone() { x = x, y = y, width = width, height = height / 2 });
        zones.Add(new Zone() { x = x, y = y + height / 2, width = width, height = height / 2 });
        break;
      case 3:
        zones.Add(new Zone() { x = x, y = y, width = width, height = height / 3 });
        zones.Add(new Zone() { x = x, y = y + height / 3, width = width, height = height / 3 });
        zones.Add(new Zone() { x = x, y = y + 2 * (height / 3), width = width, height = height / 3 });
        break;
      case 4:
        zones.Add(new Zone() { x = x, y = y, width = width, height = height / 4 });
        zones.Add(new Zone() { x = x, y = y + height / 4, width = width, height = height / 4 });
        zones.Add(new Zone() { x = x, y = y + 2 * (height / 4), width = width, height = height / 4 });
        zones.Add(new Zone() { x = x, y = y + 3 * (height / 4), width = width, height = height / 4 });
        break;
    }

    return zones;
  }

  private static bool IsPortraitOrientation(int width, int height)
  {
    return (width / height) < 1;
  }
}