using System.Collections;

namespace Sudoku
{
	public class Region : IEnumerable<Cell>
	{
		private readonly Cell[] _cells;

		public Cell this[int index] => _cells[index];

		public Region(IEnumerable<Cell> cells)
		{
			_cells = cells.ToArray();
		}

		public void CalculateCandidates()
		{
			foreach (var cell in _cells)
			{
				cell.CalculateCandidates();
			}
		}
		
		public void RemoveCandidate(int value)
		{
			foreach (var cell in _cells)
			{
				cell.Candidates.Remove(value);
			}
		}

		public IEnumerable<Cell> GetCellsWithCandidate(int candidate)
		{
			return _cells.Where(x => x.Candidates.Contains(candidate));
		}
		
		public IEnumerable<Cell> GetCellsWithAllCandidates(params int[] candidates)
		{
			return _cells.Where(x => x.Candidates.ContainsAll(candidates));
		}

		public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)_cells).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _cells.GetEnumerator();
	}
}