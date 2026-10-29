using DotNetLab.Sudoku.Techniques;

namespace DotNetLab.Sudoku;

public static class SudokuSolver
{
    public static Board Solve(Puzzle puzzle)
    {
        var board = new Board(puzzle);

        while (true)
        {
            var emptyCells = board.GetAllEmptyCells();

            if (emptyCells.Count == 0)
            {
                break;
            }

            if (Technique.NakedSingles(emptyCells))
            {
                continue;
            }

            if (Technique.HiddenSingles(emptyCells))
            {
                continue;
            }

            if (Technique.NakedPair(board))
            {
                continue;
            }

            if (Technique.HiddenPair(board))
            {
                continue;
            }

            if (Technique.Backtracking(board) is false)
            {
                throw new InvalidOperationException("Puzzle has no solution.");
            }

            break;
        }

        puzzle.Solution = board.ToAnnotationString();

        return board;
    }
}
