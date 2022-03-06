namespace Sudoku
{
	public static partial class Technique
	{
		public static bool NakedSingles(IEnumerable<Cell> emptyCells)
		{
			var nakedSingles = emptyCells.Where(cell => cell.Candidates.Count == 1).ToList();
			if (nakedSingles.Count == 0) return false;

			nakedSingles.ForEach(cell =>
			{
				cell.Value = cell.Candidates.Values.Single();
				//Debug.WriteLine($"Naked single {cell.Name}:{cell.Value}");
			});

			return true;
		}
	}
}