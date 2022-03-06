using System.Diagnostics;

namespace Sudoku
{
	public static class Program
	{
		private static readonly Stopwatch StaticStopwatch = new();

		private static float SolvePuzzle(Puzzle puzzle, int puzzleNumber = 0)
		{
			var sudoku = new SudokuSolver(puzzle);
			//Debug.WriteLine(sudoku.Board.ToDisplayString());
			StaticStopwatch.Restart();
			sudoku.Solve();
			StaticStopwatch.Stop();
			//Debug.WriteLine(sudoku.Board.ToDisplayString());
			var t = StaticStopwatch.ElapsedNanoSeconds();
			var str = puzzle.Solved ? puzzle.Solution : puzzle.Original;
			if (Technique.StepCounter < 400) return t;
			Console.WriteLine($"Puzzle #{puzzleNumber:0000000} - t={t / 1000000:0000.00} -> {puzzle.Solved} -> {str} -> StepCounter: {Technique.StepCounter}");
			return t;
		}

		private static void SolveFile(string fileName, int num)
		{
			var totalStepCounter = 0;
			var totalTime = 0f;
			var numSolved = 0;
			var lines = File.ReadLines(fileName).Take(num).ToArray();

			for (var i = 0; i < lines.Length; i++)
			{
				var line = lines[i];
				var tuple = line.Split(',');
				var puzzle = new Puzzle(tuple[0]);
				if (!puzzle.Valid) continue;
				totalTime += SolvePuzzle(puzzle, i + 1);
				totalStepCounter += Technique.StepCounter;

				if (puzzle.Solved)
				{
					numSolved++;
					if (tuple.Length > 1 && puzzle.Solution != tuple[1]) throw new Exception("Wrong solution");
				}
			}

			Console.WriteLine(new string('-', 140));
			Console.WriteLine($"Total:     - T={totalTime / 1000000:00.00}, Solved {numSolved} / {lines.Length}, Steps={totalStepCounter}");
			Console.WriteLine(new string('-', 140));
		}

		public static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();

			if (args.Length == 1)
			{
				SolvePuzzle(new Puzzle(args[0]));
				return;
			}

			//SolveFile("Puzzles\\Easy50.txt", 50);
			//SolveFile("Puzzles\\Top95.txt", 95);
			//SolvePuzzle(new Puzzle("040000179002008054006005008080070910050090030019060040300400700570100200928000060"));
			//SolveFile("C:\\Temp\\sudoku.csv", 100000);
			SolveFile("Puzzles\\49151.txt", 49151);
			Console.WriteLine($"Total execution time: {sw.ElapsedMilliseconds}");
		}
	}
}