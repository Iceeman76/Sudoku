using System.Collections;

namespace Sudoku;

public class Board : IEnumerable<Cell>
{
    public Cell this[int index] => _grid[index];
    public Region[] Rows { get; } = new Region[9];
    public Region[] Columns { get; } = new Region[9];
    public Region[] Blocks { get; } = new Region[9];

    private readonly Cell[] _grid = new Cell[81];

    public Board(Puzzle puzzle)
    {
        CreateCells();
        CreateRegions();
        SetValues(puzzle);
    }

    public List<Cell> GetAllEmptyCells()
    {
        var emptyCells = new List<Cell>(capacity: 81);

        for (var i = 0; i < 81; i++)
        {
            if (_grid[i].Value == 0) emptyCells.Add(_grid[i]);
        }

        return emptyCells;
    }

    public int GetCandidatesMask(int index)
    {
        var rowMask = Rows[index.RowIndex()].GetMaskValue();
        var columnMask = Columns[index.ColumnIndex()].GetMaskValue();
        var blockMask = Blocks[index.BlockIndex()].GetMaskValue();

        var candidatesMask = Bitmask.All.Value & ~(rowMask | columnMask | blockMask);

        return candidatesMask;
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

        for (var i = 0; i < 81; i++)
        {
            _grid[i].CreateRegions();
        }
    }

    private void SetValues(Puzzle puzzle)
    {
        for (var i = 0; i < 81; i++)
        {
            if (puzzle[i] > 0) _grid[i].Value = puzzle[i];
        }
    }

    public IEnumerator<Cell> GetEnumerator() => ((IEnumerable<Cell>)_grid).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _grid.GetEnumerator();
}
