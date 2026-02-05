using System.Diagnostics;

namespace Sudoku;

public record Line
{
    public Line(string str)
    {
        var tuple = str.Split(',');

        PuzzleString = tuple[0];
        SolutionString = tuple.Length > 1 ? tuple[1] : null;
    }

    public string PuzzleString { get; }
    public string? SolutionString { get; }
}

public static class Program
{
    private static void SolveFile(string fileName, int num, int threadCount = 1)
    {
        Console.WriteLine($"File: {fileName}");
        Console.WriteLine($"Thread count: {threadCount}");

        // TODO: LINQ
        var lines = File.ReadLines(fileName).Take(num).Select(x => new Line(x)).ToArray();
        Console.WriteLine($"Puzzle count: {lines.Length}");

        var sw = Stopwatch.StartNew();
        var interval = lines.Length / 20;

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

                //if (puzzle.Solution != lines[index].SolutionString) throw new Exception("Wrong solution");
            }
        });

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    public static void Main(string[] args)
    {
        var threadCount = args.Length >= 1 ? int.Parse(args[0]) : 4;
        var puzzleCount = args.Length >= 2 ? int.Parse(args[1]) : 400_000;

        SolveFile(@".\sudoku.csv", puzzleCount, threadCount);
    }
}
