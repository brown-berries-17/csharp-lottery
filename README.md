# C# Lottery

## Task details

Write a function `GenerateLotteryNumbers` which generates 4 random numbers between 1 and 50, and guarantees they are all different. It should return a Numbers object which is an instance of a class you have created. For example, the calling code would look something like this:

```cs
Numbers monday = GenerateLotteryNumbers();
Console.WriteLine($"Monday: {monday.n1} {monday.n2} {monday.n3} {monday.n4}");

Numbers friday = GenerateLotteryNumbers();
Console.WriteLine($"Friday: {friday.n1} {friday.n2} {friday.n3} {friday.n4}");
```

Create a second function CheckPrize which can be used like the snippet below. The prizes are: £1 for 1 matching number, £5 for 2, £50 for 3, and £250 for all 4 matching

```cs
Numbers lotto = GenerateLotteryNumbers();
int prize = CheckPrize(lotto, 23, 14, 6, 49);
Console.WriteLine($"You won £{prize}");
```

## Extra notes

Complete the task and add extra functionality to make sure you’re using what we’ve learnt so far.

Try to make it bug-free, and able to handle anything the user tries to input.