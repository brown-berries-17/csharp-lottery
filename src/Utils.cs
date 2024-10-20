using System;
using System.Linq;
using System.Collections.Generic;

static class Utils {
  public const int NL_CHAR_BUFFER = 2;

  public static string RequestLine(string msg) {
    Console.Write(msg);
    return Console.ReadLine() ?? String.Empty;
  }

  public static void WriteAndPause(string msg, string? pauseMsg = null) {
    // using Environment.NewLine instead of \n
    // to avoid potential issues in different OSes
    // where, for example, new line is represented as \r\n
    pauseMsg ??= $"{Environment.NewLine}Press any key to continue...";

    Console.Write(msg);
    Console.Write(pauseMsg);
    Console.ReadKey(true);
    ClearCurrentLine();
  }

  public static void ClearCurrentLine() {
    Console.SetCursorPosition(0, Console.CursorTop);

    // reserving some space for the newline character
    // to ensure there are no visual differences
    // because of the way different consoles handle word wrap
    Console.Write(new String(' ', Console.WindowWidth - NL_CHAR_BUFFER));

    Console.SetCursorPosition(0, Console.CursorTop);
  }

  public static string NumbersAsString(List<int> nums) {
    nums.Sort();
    var nums2 = nums.Select(num => num > 9 ? "" + num : " " + num);
    return String.Join(", ", nums2);
  }
}