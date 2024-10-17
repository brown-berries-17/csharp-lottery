class ShortLottery : Lottery {  
  public ShortLottery() : base(4, 1 ,51) {
    this.Prizes = [
      "Better luck next time!",
      "You won £1!",
      "You won £5!",
      "You won £50!",
      "JACKPOT!!! You won £250!"
    ];

    this.Name = "short lottery";
  }
}
