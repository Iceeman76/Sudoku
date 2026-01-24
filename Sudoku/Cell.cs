namespace Sudoku;

public class Cell
{
    public Region[] Regions { get; private set; }
    public Bitmask Candidates { get; set; } = Bitmask.All;
    public int Index { get; init; }

    private string Name => $"{(char)(Index.RowIndex() + 'A')}{Index.ColumnIndex() + 1}";
    private Region Row => _board.Rows[Index.RowIndex()];
    private Region Column => _board.Columns[Index.ColumnIndex()];
    private Region Block => _board.Blocks[Index.BlockIndex()];

    private int _value;
    private readonly Board _board;

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

            foreach (var region in Regions!)
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
        var candidates = _board.GetCandidates(Index);
        Candidates.SetValues(candidates);
    }

    public Cell(Board board) => _board = board;
    public void CreateRegions() => Regions = [Row, Column, Block];
    public override string ToString() => Name;
}
