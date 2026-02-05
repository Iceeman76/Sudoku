using Sudoku.Techniques;

namespace Sudoku;

public static class SudokuSolver
{
    public static Board Solve(Puzzle puzzle)
    {
        var board = new Board(puzzle);

        while (true)
        {
            // TODO: LINQ
            var emptyCells = board.GetAllEmptyCells().ToArray();

            if (emptyCells.Length == 0) break;

            if (Technique.NakedSingles(emptyCells)) continue;
            if (Technique.HiddenSingles(emptyCells)) continue;
            if (Technique.NakedPair(board)) continue;
            if (Technique.HiddenPair(board)) continue;

            Technique.Backtracking(board);

            break;
        }

        puzzle.Solution = board.ToAnnotationString();

        return board; // TODO: Bättre vi gör ett nytt objekt som heter Solution som vi kan slänga in lite olika saker i och returnera
    }
}
