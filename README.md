# C\# Lottery

## About

It’s a simple console game. Let the program generate four or six random whole numbers from 1 to 50 for a lottery game. You will then be granted an opportunity to guess the numbers. No any real prizes are granted, but you may consider yourself very lucky, especially if you guess 3+ numbers, since the chances of that happening are drastically low.

How low, exactly?

$$
  ^nC_r = \binom{n}{r} = \frac{n!}{r!(n - r)!}
$$

For our case with six numbers, it is:

$$
  ^nC_r = \binom{n}{r} = \frac{50!}{6! \times (50 - 6)!} = \frac{50!}{6! \times 44!} = 15\,890\,700
$$

So, the chances of guessing all six numbers are about 1 in 15 million. Slightly better than the national Lotto, though (1 in 45 057 474, since the poll there is from 1 to 59).

## How to run?

### Build from source code

Requirements:

- .NET SDK ver. 8.0+

Steps:

1. Clone the repository:

   ```sh
   git clone https://github.com/brown-berries-17/csharp-lottery
   ```

2. Navigate to the project root directory (where the `.csproj` file is):

    ```sh
    cd ./csharp-lottery/
    ```

3. Now you have two options:

    - Execute the `dotnet run` command to build and run the program in debug mode;
    - Execute the `dotnet publish` command to create binary executables from the source code. Executables can later be found in the `bin/Release/net8.0/{your_os}/publish/` directory. Just run it.

### Download and run the compiled program

1. Navigate to the <https://github.com/brown-berries-17/csharp-lottery/releases>;
2. Download the suitable for your system executable (x64 Linux or Windows OS; No Mac OS, pardon me, try building from the source);
3. Run the executable. Installation and/or root (administrative) privileges shall not be required.
