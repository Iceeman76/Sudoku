using System.Collections;

namespace Sudoku;

public sealed class Region(Cell[] cells) : IEnumerable<Cell>
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

        foreach (var cell in cells)
        {
            if (cell.Value > 0)
            {
                mask |= 1 << cell.Value;
            }
        }

        return mask;
    }

    public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)cells).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => cells.GetEnumerator();
}
