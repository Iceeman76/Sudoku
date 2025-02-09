using System.Collections;

namespace Sudoku;

public class Board : IEnumerable<Cell>
{
    public Cell this[int index] => _grid[index];
    public Region[] Rows { get; } = new Region[9];
    public Region[] Columns { get; } = new Region[9];
    public Region[] Blocks { get; } = new Region[9];

    private readonly Cell[] _grid = new Cell[81];
    private static readonly int[] OneToNine = [1, 2, 3, 4, 5, 6, 7, 8, 9];

    public Board(Puzzle puzzle)
    {
        CreateCells();
        CreateRegions();
        SetValues(puzzle);
    }

    public IEnumerable<Cell> GetAllEmptyCells() => _grid.Where(x => x.Value == 0);

    public IEnumerable<int> GetCandidates(int index)
    {
        var row = Rows[index.RowIndex()].Where(x => x.Value > 0).Select(x => x.Value);
        var column = Columns[index.ColumnIndex()].Where(x => x.Value > 0).Select(x => x.Value);
        var block = Blocks[index.BlockIndex()].Where(x => x.Value > 0).Select(x => x.Value);

        var candidates = OneToNine
            .Except(row)
            .Except(column)
            .Except(block);

        return candidates;
    }

    private void CreateCells()
    {
        for (var i = 0; i < 81; i++)
        {
            _grid[i] = new Cell(this) { Index = i };
        }
    }

    private void CreateRegions()
    {
        for (var i = 0; i < 9; i++)
        {
            Rows[i] = _grid.CreateRow(i);
            Columns[i] = _grid.CreateColumn(i);
            Blocks[i] = _grid.CreateBlock(i);
        }
    }

    private void SetValues(Puzzle puzzle)
    {
        for (var i = 0; i < 81; i++)
        {
            this[i].CreateRegions();
            if (puzzle[i] > 0) this[i].Value = puzzle[i];
        }
    }

    public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)_grid).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _grid.GetEnumerator();
}
