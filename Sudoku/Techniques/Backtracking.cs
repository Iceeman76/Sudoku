namespace Sudoku.Techniques;

public static partial class Technique
{
    public static bool Backtracking(Board board)
    {
        var cell = board.GetAllEmptyCells().OrderBy(x => x.Candidates.Count).FirstOrDefault();

        if (cell == null) return true;
        if (cell.Candidates.Count == 0) return false;

        foreach (var value in cell.Candidates.Values)
        {
            cell.Value = value;

            if (Backtracking(board)) return true;

            cell.Value = 0;
        }

        return false;
    }
}
