namespace Sudoku
{
	public class SudokuSolver
	{
		public readonly Board Board;
		private readonly Puzzle _puzzle;

		public SudokuSolver(Puzzle puzzle)
		{
			Technique.StepCounter = 0;
			Board = new Board(puzzle);
			_puzzle = puzzle;
		}

		public void Solve()
		{
			while (true)
			{
				var emptyCells = Board.GetAllEmptyCells().ToArray();
				if (emptyCells.Length == 0) break;

				// if (Technique.NakedSingles(emptyCells)) continue;
				// if (Technique.HiddenSingles(emptyCells)) continue;
				//
				// if (Technique.NakedPair(Board)) continue;
				// if (Technique.HiddenPair(Board)) continue;

				Technique.Backtracking(Board);

				break;
			}

			_puzzle.Solution = Board.ToAnnotationString();
		}
	}
}