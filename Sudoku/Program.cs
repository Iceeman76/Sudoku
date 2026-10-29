using System.Diagnostics;

namespace DotNetLab.Sudoku;

public sealed record Line
{
    public Line(string str) => PuzzleString = str.Split(',')[0];

    public string PuzzleString { get; }
}

public static class Program
{
    private static void SolveFile(string fileName, int num, int threadCount = 1)
    {
        Console.WriteLine($"File: {fileName}");
        Console.WriteLine($"Thread count: {threadCount}");

        var lines = File.ReadLines(fileName).Take(num).Select(line => new Line(line)).ToArray();
        Console.WriteLine($"Puzzle count: {lines.Length}");

        var sw = Stopwatch.StartNew();
        var interval = Math.Max(1, lines.Length / 20);

        Parallel.For(0, threadCount, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, threadIndex =>
        {
            for (var index = threadIndex; index < lines.Length; index += threadCount)
            {
                var puzzle = new Puzzle(lines[index].PuzzleString);

                SudokuSolver.Solve(puzzle);

                if (index % interval == 0)
                {
                    Console.WriteLine($"Puzzle #{index:0000000} - {puzzle.Solution}");
                }
            }
        });

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    public static void Main(string[] args)
    {
        var puzzleFile = args[0];

        var threadCount = args.Length >= 2 ? int.Parse(args[1]) : 4;
        var puzzleCount = args.Length >= 3 ? int.Parse(args[2]) : 400_000;

        SolveFile(puzzleFile, puzzleCount, threadCount);
    }
}
