namespace DotNetLab.Sudoku.Techniques;

public static partial class Technique
{
    public static bool NakedPair(Board board)
    {
        for (var index = 0; index < 9; index++)
        {
            if (FindNakedPair(board.Blocks[index]) || FindNakedPair(board.Rows[index]) || FindNakedPair(board.Columns[index]))
            {
                return true;
            }
        }

        return false;
    }

    private static bool FindNakedPair(Region region)
    {
        for (var firstIndex = 0; firstIndex < 8; firstIndex++)
        {
            var firstCell = region[firstIndex];

            if (firstCell.Candidates.Count is not 2)
            {
                continue;
            }

            var pairMask = firstCell.Candidates.Value;
            var matchingCells = 0;
            var secondIndex = -1;

            for (var index = 0; index < 9; index++)
            {
                if (region[index].Candidates.Value == pairMask)
                {
                    matchingCells++;

                    if (index > firstIndex)
                    {
                        secondIndex = index;
                    }
                }
            }

            if (matchingCells is not 2 || secondIndex < 0)
            {
                continue;
            }

            var changed = false;

            for (var index = 0; index < 9; index++)
            {
                if (index == firstIndex || index == secondIndex)
                {
                    continue;
                }

                var candidates = region[index].Candidates;

                if ((candidates.Value & pairMask) is not 0)
                {
                    candidates.RemoveMask(pairMask);
                    changed = true;
                }
            }

            if (changed)
            {
                return true;
            }
        }

        return false;
    }
}
