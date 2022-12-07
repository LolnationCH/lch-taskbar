ScreenFetcher screenFetcher = new ScreenFetcher();
var t = screenFetcher.GetAllWindowsInScreens();
if (t.Count > 0)
{
  foreach (var screenData in t)
  {
    screenData._containers[0].OrganizeWindows();
  }
}