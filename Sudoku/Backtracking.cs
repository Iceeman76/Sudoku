namespace Sudoku
{
	public static partial class Technique
	{
		public static int StepCounter { get; set; }

		public static void Backtracking(Board board)
		{
			Solve(board);
		}

		private static bool Solve(Board board)
		{
			var cell = board.GetAllEmptyCells().OrderBy(x => x.Candidates.Count).FirstOrDefault();
			if (cell == null) return true;
			if (cell.Candidates.Count == 0) return false;
		
			foreach (var value in cell.Candidates.Values)
			{
				StepCounter++;
		
				cell.Value = value;
		
				if (Solve(board)) return true;
		
				cell.Value = 0;
			}
		
			return false;
		}
	}
}