using System.Collections;

namespace Sudoku;

public class Region(Cell[] cells) : IEnumerable<Cell>
{
    public Cell this[int index] => cells[index];

    public void CalculateCandidates()
    {
        foreach (var cell in cells)
        {
            cell.CalculateCandidates();
        }
    }

    public void RemoveCandidate(int value)
    {
        foreach (var cell in cells)
        {
            cell.Candidates.Remove(value);
        }
    }

    public int GetMaskValue()
    {
        var mask = 0;

        for (var i = 0; i < 9; i++)
        {
            if (cells[i].Value > 0) mask |= 1 << cells[i].Value;
        }

        return mask;
    }

    public IEnumerable<Cell> GetCellsWithCandidate(int candidate)
    {
        return cells.Where(x => x.Candidates.Contains(candidate));
    }

    public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)cells).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => cells.GetEnumerator();
}
