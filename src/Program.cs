using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic; 

class Program {
  static void Main(string[] args) {
    bool keepPlaying = true;

    while (keepPlaying) {
      Utils.WriteAndPause("Welcome, gambler. Back for another game?");
      Console.WriteLine("Would you like to play a standard lottery with six numbers or a short one with four?");

      string usrMode = Utils.RequestLine("Your answer: ");
      usrMode = usrMode.Trim().ToLower();

      var modes = new Dictionary<string, HashSet<string>> {
        { "standard", ["standard", "long", "hard", "normal", "six", "6", "1", "l"] },
        { "short", ["short", "easy", "four", "4", "2", "s"] }
      };

      Lottery lottery;

      while (true) {
        if ( modes["short"].Contains(usrMode) ) {
          usrMode = "short";
          lottery = new ShortLottery();
          break;
        } else if ( modes["standard"].Contains(usrMode) ) {
          usrMode = "standard";
          lottery = new StandardLottery();
          break;
        }

        Console.WriteLine(
          $"\"{usrMode}\" is not a valid option. Enter “standard” or “hard”" +
          $"to play a lottery with six numbers OR “short” or “easy”" +
          $"to play a lottery with four numbers."
        );
        usrMode = Utils.RequestLine("Your answer: ");
        usrMode = usrMode.Trim().ToLower();
      }

      Utils.WriteAndPause($"Alright, let’s play a {lottery.Name}.");
      OutputGeneratingNumbers(lottery.Numbers.Count);
      List<int> usrNums = RequestAndValidateNumbers(lottery.Numbers.Count);
      string usrNumsAsString = Utils.NumbersAsString(usrNums);
      Utils.WriteAndPause("The moment of truth...");

      int matches = lottery.CheckMatches(usrNums);

      Utils.WriteAndPause($"Winning numbers: { lottery.NumbersAsString }");
      Utils.WriteAndPause($"Your numbers:    { usrNumsAsString }");
      Utils.WriteAndPause(
        $"You guessed {matches}/{lottery.Numbers.Count}. " +
        lottery.Prizes[matches]
      );

      do {
        Console.Write("Press “Q” to exit or “Enter” to continue playing: ");
        var usrOption = Console.ReadKey();
        
        Console.WriteLine(String.Empty);

        if (usrOption.Key == ConsoleKey.Q) {
          Console.WriteLine("Thanks for playing. Se you later!");
          keepPlaying = false;
          break;
        } else if (usrOption.Key == ConsoleKey.Enter) {
          Console.WriteLine(String.Empty);
          break;
        } else {
          Console.WriteLine($"{usrOption.KeyChar.ToString()} is not a valid option.");
        }

      } while (true);
    }

    void OutputGeneratingNumbers(int maxNums) {
      string mask = String.Empty;
      string nums = String.Empty;
      Lottery tempLottery;

      for (int i = 0; i < maxNums; i++) {
        mask += "##";
        if (i != maxNums - 1) {
          mask += ", ";
        }
      }

      for (int i = 0; i < mask.Length; i++) {
        if ( maxNums == 4 ) {
          tempLottery = new ShortLottery();
          nums = tempLottery.NumbersAsString;
        } else if ( maxNums == 6 ) {
          tempLottery = new StandardLottery();
          nums = tempLottery.NumbersAsString;
        }

        Console.Write("Generating lottery numbers... ");
        Console.Write( mask.Substring(0, i) );
        Console.Write( nums.Substring(i, nums.Length - i) );
        Console.Write(".");
        Thread.Sleep(250);
        Console.SetCursorPosition(0, Console.CursorTop);
      }

      Console.WriteLine($"Generating lottery numbers... {mask}. Finished!");
    }

    List<int> RequestAndValidateNumbers(int maxNums) {
      string[] usrNums;
      List<int> usrNums2;

      while (true) {
        usrNums = Utils.RequestLine("Now enter your guesses separated by comma: ").Split(",");

        // validation for length
        if (usrNums.Length != (maxNums)) {
          Console.WriteLine(
            $"You must enter {maxNums} integers separated by comma."
          );
          continue;
        }

        // validation for integers
        try {
          usrNums2 = usrNums.Select(numStr => Convert.ToInt32(numStr)).ToList();
        } catch {
          Console.WriteLine(
            $"You must enter { maxNums } integers separated by comma."
          );
          continue;
        }

        // validation for number range
        if (usrNums2.Any(usrNum => usrNum < 1 || usrNum > 50)) {
          Console.WriteLine("Your numbers must be within the range 1-50.");
          continue;
        }

        // validation for uniqueness
        if (usrNums2.Count != new HashSet<int>(usrNums2).Count) {
          Console.WriteLine("All your numbers must be unique.");
          continue;
        }

        break;
      }

      return usrNums2;
    }
  }
}
