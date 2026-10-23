using System.Numerics;

namespace Sudoku.Techniques;

public static partial class Technique
{
    public static bool Backtracking(Board board)
    {
        var values = new int[81];
        var rowMasks = new int[9];
        var columnMasks = new int[9];
        var blockMasks = new int[9];

        for (var index = 0; index < 81; index++)
        {
            var value = board[index].Value;
            values[index] = value;

            if (value == 0)
            {
                continue;
            }

            var valueMask = 1 << value;
            var row = Definitions.RowIndex[index];
            var column = Definitions.ColumnIndex[index];
            var block = Definitions.BlockIndex[index];

            if (((rowMasks[row] | columnMasks[column] | blockMasks[block]) & valueMask) is not 0)
            {
                return false;
            }

            rowMasks[row] |= valueMask;
            columnMasks[column] |= valueMask;
            blockMasks[block] |= valueMask;
        }

        if (TrySolve(board, values, rowMasks, columnMasks, blockMasks) is false)
        {
            return false;
        }

        for (var index = 0; index < 81; index++)
        {
            if (board[index].Value == 0)
            {
                board[index].Value = values[index];
            }
        }

        return true;
    }

    private static bool TrySolve(Board board, int[] values, int[] rowMasks, int[] columnMasks, int[] blockMasks)
    {
        var selectedIndex = -1;
        var selectedCandidates = 0;
        var fewestCandidates = 10;

        for (var index = 0; index < 81; index++)
        {
            if (values[index] is not 0)
            {
                continue;
            }

            var usedMask = rowMasks[Definitions.RowIndex[index]] | columnMasks[Definitions.ColumnIndex[index]] | blockMasks[Definitions.BlockIndex[index]];
            var candidates = board[index].Candidates.Value & ~usedMask;
            var candidateCount = BitOperations.PopCount((uint)candidates);

            if (candidateCount == 0)
            {
                return false;
            }

            if (candidateCount < fewestCandidates)
            {
                selectedIndex = index;
                selectedCandidates = candidates;
                fewestCandidates = candidateCount;

                if (candidateCount == 1)
                {
                    break;
                }
            }
        }

        if (selectedIndex < 0)
        {
            return true;
        }

        var row = Definitions.RowIndex[selectedIndex];
        var column = Definitions.ColumnIndex[selectedIndex];
        var block = Definitions.BlockIndex[selectedIndex];

        for (var value = 1; value <= 9; value++)
        {
            var valueMask = 1 << value;

            if ((selectedCandidates & valueMask) == 0)
            {
                continue;
            }

            values[selectedIndex] = value;
            rowMasks[row] |= valueMask;
            columnMasks[column] |= valueMask;
            blockMasks[block] |= valueMask;

            if (TrySolve(board, values, rowMasks, columnMasks, blockMasks))
            {
                return true;
            }

            values[selectedIndex] = 0;
            rowMasks[row] &= ~valueMask;
            columnMasks[column] &= ~valueMask;
            blockMasks[block] &= ~valueMask;
        }

        return false;
    }
}
