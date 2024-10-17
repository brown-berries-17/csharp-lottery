class StandardLottery : Lottery {
  public StandardLottery() : base(6, 1, 51) {
    this.Prizes = [
      "Better luck next time!",
      "You won £5!",
      "You won £50!",
      "You won £300!",
      "You won £1,000!",
      "Big win! £10,000",
      "JACKPOT!!! £1,000,000"
    ];

    this.Name = "standard lottery";
  }
}
