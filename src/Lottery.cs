using System;
using System.Collections.Generic;

class Lottery {
  private readonly HashSet<int> numbers = [];
  public List<int> Numbers {
    get {
      var nums = new List<int>(this.numbers);
      nums.Sort();
      return nums;
    }
  }
  public string NumbersAsString {
    get {
      return Utils.NumbersAsString(this.Numbers);
    }
  }

  private string[] prizes = [];
  public string[] Prizes {
    get { return this.prizes; }
    set { this.prizes = value; }
  }

  private string name = String.Empty;
  public string Name {
    get { return this.name; }
    set { this.name = value; }
  }

  public Lottery(int maxNums, int minNum, int maxNum) {
    // Random rng = new Random();

    for (int i = 0; i < maxNums; i++) {
      int rndNum;

      do {
        // Random.Next() is not thread safe
        // it breaks if the same instance is called from different threads at the same time
        // I know I don’t work much with threading in this project,
        // but it’s good to know for the future
        rndNum = Random.Shared.Next(minNum, maxNum);
      } while (numbers.Contains(rndNum));

      this.numbers.Add(rndNum);
    }
  }

  public int CheckMatches(List<int> usrNums) {
    int matches = 0;

    foreach (int usrNum in usrNums) {
      if (this.numbers.Contains(usrNum)) {
        matches++;
      }
    }

    return matches;
  }
}