using System.Diagnostics;

namespace Sudoku
{
	public static partial class Technique
	{
		public static bool NakedPair(Board board)
		{
			for (var i = 0; i < 9; i++)
			{
				var foundNakedPair = FindNakedPair(board.Blocks[i]) ||
									 FindNakedPair(board.Rows[i]) ||
									 FindNakedPair(board.Columns[i]);

				if (foundNakedPair) return true;
			}

			return false;
		}

		private static bool FindNakedPair(Region region)
		{
			//TODO: Clean up this code and do not depend on the number 2 because we will need more later
			var c = region
				.Where(x => x.Candidates.Count == 2)
				.GroupBy(x => x.Candidates.Value)
				.Where(g => g.Count() == 2)
				.ToArray();

			if (c.Length == 0) return false;

			var cells = c[0].ToArray();
			var candidates = cells[0].Candidates.Values.ToArray();

			var y1 = region.GetCellsWithCandidate(candidates[0]).Except(cells).ToList();
			y1.ForEach(x => x.Candidates.Remove(candidates[0]));
			var y2 = region.GetCellsWithCandidate(candidates[1]).Except(cells).ToList();
			y2.ForEach(x => x.Candidates.Remove(candidates[1]));
			if (y1.Count == 0 && y2.Count == 0) return false;
			Debug.WriteLine($"Naked pair ({cells[0].Name}, {cells[1].Name}): ({string.Join(", ", candidates)})");
			return true;
		}
	}
}