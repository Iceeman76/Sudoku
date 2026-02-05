using System.Numerics;

namespace Sudoku.Techniques;

public static partial class Technique
{
    public static bool NakedSingles(Cell[] emptyCells)
    {
        var nakedSingleFound = false;

        foreach (var cell in emptyCells)
        {
            if (cell.Candidates.Count != 1) continue;
            cell.Value = BitOperations.TrailingZeroCount(cell.Candidates.Value);
            nakedSingleFound = true;
        }

        return nakedSingleFound;
    }
}
