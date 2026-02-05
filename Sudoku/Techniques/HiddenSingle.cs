namespace Sudoku.Techniques;

public static partial class Technique
{
    public static bool HiddenSingles(IEnumerable<Cell> emptyCells)
    {
        foreach (var cell in emptyCells)
        {
            if (cell.Candidates.Count <= 1) continue;

            for (var value = 1; value <= 9; value++)
            {
                if (cell.Candidates.Contains(value) && FindHiddenSingle(cell, value))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool FindHiddenSingle(Cell cell, int value)
    {
        foreach (var region in cell.Regions)
        {
            var count = 0;

            for (var i = 0; i < 9; i++)
            {
                if (region[i].Candidates.Contains(value))
                {
                    count++;
                }
            }

            if (count != 1) continue;
            cell.Value = value;

            return true;
        }

        return false;
    }
}
