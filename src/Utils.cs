static class Utils {
  public static string RequestLine(string msg) {
    Console.Write(msg);
    return Console.ReadLine() ?? String.Empty;
  }

  public static void WriteAndPause(string msg, string pauseMsg = "\nPress “Enter” to continue...") {
    Console.Write(msg);
    Console.Write(pauseMsg);
    Console.ReadKey(true);
    ClearCurrentLine();
  }

  public static void ClearCurrentLine() {
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new String(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, Console.CursorTop);
  }

  public static string NumbersAsString(List<int> nums) {
    nums.Sort();
    var nums2 = nums.Select(num => num > 9 ? "" + num : "0" + num);
    return String.Join(", ", nums2);
  }
}