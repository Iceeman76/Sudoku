using System.Numerics;

namespace DotNetLab.Sudoku.Techniques;

public static partial class Technique
{
    public static bool NakedSingles(IEnumerable<Cell> emptyCells)
    {
        var nakedSingleFound = false;

        foreach (var cell in emptyCells)
        {
            if (cell.Candidates.Count is not 1)
            {
                continue;
            }

            cell.Value = BitOperations.TrailingZeroCount(cell.Candidates.Value);
            nakedSingleFound = true;
        }

        return nakedSingleFound;
    }
}
