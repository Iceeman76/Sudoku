namespace Sudoku;

public class Cell(Board board)
{
    public Region[] Regions = null!;
    public Bitmask Candidates = Bitmask.All;
    public int Index;

    private string Name => $"{(char)(Index.RowIndex() + 'A')}{Index.ColumnIndex() + 1}";
    private Region Row => board.Rows[Index.RowIndex()];
    private Region Column => board.Columns[Index.ColumnIndex()];
    private Region Block => board.Blocks[Index.BlockIndex()];

    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            if (value == 0)
            {
                Clear();
                return;
            }

            _value = value;

            Candidates.Clear();

            foreach (var region in Regions)
            {
                region.RemoveCandidate(value);
            }
        }
    }

    private void Clear()
    {
        // Use this only when backtracking is applied
        _value = 0;

        foreach (var region in Regions)
        {
            region.CalculateCandidates();
        }
    }

    public void CalculateCandidates()
    {
        Candidates.Clear();
        if (Value > 0) return;

        var mask = board.GetCandidatesMask(Index);
        Candidates.SetMask(mask);
    }

    public void CreateRegions() => Regions = [Row, Column, Block];
    public override string ToString() => Name;
}
