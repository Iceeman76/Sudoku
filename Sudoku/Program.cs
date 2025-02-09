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
    private static void SolveFileA(string fileName, int num, int threadCount = 1)
    {
        var lines = File.ReadLines(fileName).Take(num).Select(x => new Line(x)).ToArray();
        var sw = Stopwatch.StartNew();

        Parallel.ForEach(lines, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, (line, _, index) =>
        {
            var puzzle = new Puzzle(line.PuzzleString);

            SudokuSolver.Solve(puzzle);

            if (index % 10000 == 0)
            {
                Console.WriteLine($"{Environment.CurrentManagedThreadId:00}: Puzzle #{index:0000000} - {puzzle.Solution}");
            }

            //if (tuple.Length > 1 && puzzle.Solution != tuple[1]) throw new Exception("Wrong solution");
        });

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    private static void SolveFileB(string fileName, int num, int threadCount = 1)
    {
        var lines = File.ReadLines(fileName).Take(num).Select(x => new Line(x)).ToArray();
        var sw = Stopwatch.StartNew();

        Parallel.For(0, threadCount, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, threadIndex =>
        {
            for (var index = threadIndex; index < lines.Length; index += threadCount)
            {
                var puzzle = new Puzzle(lines[index].PuzzleString);

                SudokuSolver.Solve(puzzle);

                if (index % 10000 == 0)
                {
                    Console.WriteLine($"{Environment.CurrentManagedThreadId:00}: Puzzle #{index:0000000} - {puzzle.Solution}");
                }

                //if (tuple.Length > 1 && puzzle.Solution != tuple[1]) throw new Exception("Wrong solution");
            }
        });

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    private static async Task SolveFileC(string fileName, int num, int threadCount)
    {
        var lines = File.ReadLines(fileName).Take(num).Select(x => new Line(x)).ToArray();
        var sw = Stopwatch.StartNew();

        var tasks = new List<Task>();

        for (var threadIndex = 0; threadIndex < threadCount; threadIndex++)
        {
            var indexCopy = threadIndex;

            tasks.Add(Task.Run(() =>
            {
                for (var index = indexCopy; index < lines.Length; index += threadCount)
                {
                    var puzzle = new Puzzle(lines[index].PuzzleString);

                    SudokuSolver.Solve(puzzle);

                    if (index % 10000 == 0)
                    {
                        Console.WriteLine($"{Environment.CurrentManagedThreadId:00}: Puzzle #{index:0000000} - {puzzle.Solution}");
                    }

                    //if (tuple.Length > 1 && puzzle.Solution != tuple[1]) throw new Exception("Wrong solution");
                }
            }));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    private static void SolveFileD(string fileName, int num)
    {
        var lines = File.ReadLines(fileName).Take(num).Select(x => new Line(x)).ToArray();
        var sw = Stopwatch.StartNew();

        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index];
            var puzzle = new Puzzle(line.PuzzleString);

            SudokuSolver.Solve(puzzle);

            if (index % 10000 == 0)
            {
                Console.WriteLine($"{Environment.CurrentManagedThreadId:00}: Puzzle #{index:0000000} - {puzzle.Solution}");
            }

            //if (tuple.Length > 1 && puzzle.Solution != tuple[1]) throw new Exception("Wrong solution");
        }

        Console.WriteLine(new string('-', 104));
        Console.WriteLine($"Total solution time (s): {sw.ElapsedMilliseconds / 1000.0}");
        Console.WriteLine(new string('-', 104));
    }

    public static void Main()
    {
        //SolveFile("Puzzles\\Easy50.txt", 50);
        //SolveFile("Puzzles\\Top95.txt", 95);
        //SolvePuzzle(new Puzzle("040000179002008054006005008080070910050090030019060040300400700570100200928000060"));
        //SolveFile("Puzzles\\49151.txt", 49151);

        SolveFileA(@"C:\Temp\sudoku.csv", 100_000, 4);
        SolveFileB(@"C:\Temp\sudoku.csv", 100_000, 4);
        SolveFileC(@"C:\Temp\sudoku.csv", 100_000, 4).GetAwaiter().GetResult();
        SolveFileD(@"C:\Temp\sudoku.csv", 100_000);
    }
}
