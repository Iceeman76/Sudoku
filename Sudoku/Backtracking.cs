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
			var c = board.GetAllEmptyCells().OrderBy(x => x.Candidates.Count).FirstOrDefault();
			if (c == null) return true;
			if (c.Candidates.Count == 0) return false;
		
			foreach (var value in c.Candidates.Values)
			{
				StepCounter++;
		
				c.Value = value;
		
				if (Solve(board)) return true;
		
				c.Value = 0;
			}
		
			return false;
		}
	}
}