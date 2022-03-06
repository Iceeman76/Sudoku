namespace Sudoku
{
	public static partial class Technique
	{
		public static bool HiddenPair(Board board)
		{
			for (var i = 0; i < 9; i++)
			{
				var foundHiddenPair = FindHiddenPair(board.Blocks[i]) ||
									  FindHiddenPair(board.Rows[i]) ||
									  FindHiddenPair(board.Columns[i]);

				if (foundHiddenPair) return true;
			}

			return false;
		}

		private static bool FindHiddenPair(Region region)
		{
			//TODO: Clean up this code and do not depend on the number 2 because we will need more later
			for (var i = 0; i < 8; i++)
			{
				for (var j = i + 1; j < 9; j++)
				{
					var cells = new[] { region[i], region[j] };
					if (cells[0].Candidates.Count < 3 && cells[1].Candidates.Count < 3) continue;
					var common = cells[0].Candidates & cells[1].Candidates;
					if (common.Count < 2) continue;
					var candidates = common.Values.ToList(); // TODO: Gör om detta.. kan kolla på bitmask istället

					for (var k = candidates.Count - 1; k >= 0; k--)
					{
						var candidate = candidates[k];
						var y = region.GetCellsWithCandidate(candidate).Except(cells);
						if (y.Any()) candidates.Remove(candidate);
					}

					if (candidates.Count != 2) continue;
					cells[0].Candidates = new Bitmask().SetValue(candidates[0]).SetValue(candidates[1]);
					cells[1].Candidates = new Bitmask().SetValue(candidates[0]).SetValue(candidates[1]);
					//Debug.WriteLine($"Hidden pair ({cells[0].Name}, {cells[1].Name}): ({string.Join(", ", candidates)})");
					return true;
				}
			}

			return false;
		}
	}
}