namespace Sudoku
{
	public static partial class Technique
	{
		public static bool HiddenSingles(IEnumerable<Cell> emptyCells)
		{
			var cells = emptyCells.Where(x => x.Candidates.Count > 1);

			foreach (var cell in cells)
			{
				if (cell.Candidates.Values.Any(value => FindHiddenSingle(cell, value)))
				{
					return true;
				}
			}

			return false;
		}

		private static bool FindHiddenSingle(Cell cell, int value)
		{
			foreach (var region in cell.Regions!)
			{
				if (region.GetCellsWithCandidate(value).Count() != 1) continue;
				cell.Value = value;
				//Debug.WriteLine($"Hidden single {cell.Name}:{value}");
				return true;
			}

			return false;
		}
	}
}