using System.Numerics;

namespace DotNetLab.Sudoku.Techniques;

public static partial class Technique
{
    public static bool HiddenPair(Board board)
    {
        for (var index = 0; index < 9; index++)
        {
            if (FindHiddenPair(board.Blocks[index]) || FindHiddenPair(board.Rows[index]) || FindHiddenPair(board.Columns[index]))
            {
                return true;
            }
        }

        return false;
    }

    private static bool FindHiddenPair(Region region)
    {
        for (var firstIndex = 0; firstIndex < 8; firstIndex++)
        {
            var firstCell = region[firstIndex];

            for (var secondIndex = firstIndex + 1; secondIndex < 9; secondIndex++)
            {
                var secondCell = region[secondIndex];

                if (firstCell.Candidates.Count < 3 && secondCell.Candidates.Count < 3)
                {
                    continue;
                }

                var pairMask = firstCell.Candidates.Value & secondCell.Candidates.Value;

                if (BitOperations.PopCount((uint)pairMask) < 2)
                {
                    continue;
                }

                for (var index = 0; index < 9; index++)
                {
                    if (index == firstIndex || index == secondIndex)
                    {
                        continue;
                    }

                    pairMask &= ~region[index].Candidates.Value;
                }

                if (BitOperations.PopCount((uint)pairMask) is not 2)
                {
                    continue;
                }

                firstCell.Candidates.Intersect(pairMask);
                secondCell.Candidates.Intersect(pairMask);

                return true;
            }
        }

        return false;
    }
}
