using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic; 

class Program {
  static void Main(string[] args) {
    const int MIN_LOTTERY_INT = 1;
    const int MAX_LOTTERY_INT = 51;

    string[] numberWords = [
      "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    ];

    bool keepPlaying = true;

    while (keepPlaying) {
      Utils.WriteAndPause("Welcome, gambler. Back for another game?");
      Console.WriteLine("Choose your preferred game mode:");
      Console.WriteLine("1. A standard lottery with six numbers;");
      Console.WriteLine("2. A shorter one with four number.");

      string usrMode = Utils.RequestLine("Your answer: ");
      usrMode = usrMode.Trim().ToLower();

      var modes = new Dictionary<string, HashSet<string>> {
        { "standard", ["standard", "long", "l", "hard", "normal", "six", "6", "1", "1."] },
        { "short", ["short", "s", "easy", "four", "4", "2", "2."] }
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

        Console.WriteLine();
        Console.WriteLine($"“{usrMode}” is not a valid option.");
        Console.WriteLine($"Enter “standard” or “1” to play a lottery with six numbers OR");
        Console.WriteLine($"“short” or “2” to play a lottery with four numbers.");

        usrMode = Utils.RequestLine("Your answer: ");
        usrMode = usrMode.Trim().ToLower();
      }

      Console.WriteLine(); // blank line separator

      Utils.WriteAndPause($"Alright, let’s play a {lottery.Name}.");
      OutputGeneratingNumbers(lottery.Numbers.Count);

      List<int> usrNums = [];
      string usrNumsAsString = String.Empty;

      do {
        try {
          usrNums = RequestAndValidateNumbers(lottery.Numbers.Count);
          usrNumsAsString = Utils.NumbersAsString(usrNums);
          break;
        } catch (Exception e) {
          Console.WriteLine(e.Message);
        }
      } while (true);      
      
      Console.WriteLine(); // blank line separator

      Utils.WriteAndPause("The moment of truth...");

      int matches = lottery.CheckMatches(usrNums);

      Utils.WriteAndPause($"Winning numbers: { lottery.NumbersAsString }");
      Utils.WriteAndPause($"Your numbers:    { usrNumsAsString }");
      Utils.WriteAndPause(
        $"You guessed {matches}/{lottery.Numbers.Count}. " +
        lottery.Prizes[matches]
      );

      Console.WriteLine(); // blank line separator

      do {
        Console.Write("Press “Q” to exit or “Enter” to continue playing");
        var usrOption = Console.ReadKey(true);
        
        Console.WriteLine();

        if (usrOption.Key == ConsoleKey.Q) {
          Console.WriteLine("Thanks for playing. See you later!");
          keepPlaying = false;

          Thread.Sleep(2000); // prevent immediate window close

          break;
        } else if (usrOption.Key == ConsoleKey.Enter) {
          Console.WriteLine();
          break;
        } else {
          Console.WriteLine($"“{usrOption.KeyChar}” is not a valid option.");
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
      string[] usrNums = [];
      List<int> usrNums2 = [];

      usrNums = Utils.RequestLine("Now enter your guesses separated by comma: ").Split(",");

      // validation for length
      if (usrNums.Length != maxNums) {
        throw new FormatException(
          $"You must enter {numberWords[maxNums]} integers separated by comma."
        );
      }

      // validation for integers
      try {
        usrNums2 = usrNums.Select(numStr => Convert.ToInt32(numStr)).ToList();
      } catch (FormatException e) {
        // re-throwing the same exception with a custom message
        throw new FormatException(
          $"You must enter {numberWords[maxNums]} integers separated by comma.",
          e
        );
      } catch (OverflowException e) {
        throw new OverflowException(
          $"Your numbers must be between {MIN_LOTTERY_INT} and {MAX_LOTTERY_INT}.",
          e
        );
      }

      // validation for number range
      if (usrNums2.Any(usrNum => usrNum < MIN_LOTTERY_INT || usrNum > MAX_LOTTERY_INT)) {
        throw new FormatException(
          $"Your numbers must be between {MIN_LOTTERY_INT} and {MAX_LOTTERY_INT}."
        );
      }

      // validation for uniqueness
      if (usrNums2.Count != new HashSet<int>(usrNums2).Count) {
        throw new FormatException(
          $"All your numbers must be unique."
        );
      }

      return usrNums2;
    }
  }
}
