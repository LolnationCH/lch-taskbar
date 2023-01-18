using System.Runtime.InteropServices;

static class LogicHandler
{
  [DllImport("user32.dll")]
  private static extern IntPtr GetForegroundWindow();

  public static bool IsDebug = true;

  private static Keys[] _hyperKey = new Keys[]
  {
    Keys.LControlKey,
    Keys.LShiftKey,
    Keys.LMenu,
    Keys.LWin,
  };

  private static void PrintPressedKeys(Dictionary<Keys, bool> keyCombination)
  {
    if (IsDebug)
      Console.WriteLine(String.Join(", ", keyCombination.Where(x => x.Value)
                                                                                  .Select(x => x.Key.ToString())));
  }

  private static bool HasHyperKeyPressed(Dictionary<Keys, bool> keyCombination)
  {
    foreach (var key in _hyperKey)
    {
      if (!keyCombination[key])
        return false;
    }
    return true;
  }

  public static bool HandleKeyCombination(Dictionary<Keys, bool> keyCombination)
  {
    // print key combination
    PrintPressedKeys(keyCombination);

    if (!HasHyperKeyPressed(keyCombination))
      return false;

    // handle key combination
    // If users press 'h' key, toggle taskbar
    if (keyCombination[Keys.H])
    {
      WindowsTaskbar.Toggle();
      LCHTaskbarHandler.Toggle();
      return true;
    }

    return false;
  }
}