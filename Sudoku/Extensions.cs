using System.Diagnostics;
using System.Text;

namespace Sudoku
{
	public static class Extensions
	{
		private static readonly float TickLength = 1e9f / Stopwatch.Frequency;
		public static float ElapsedNanoSeconds(this Stopwatch sw) => sw.ElapsedTicks * TickLength;

		public static int RowIndex(this int index) => index / 9;
		public static int ColumnIndex(this int index) => index % 9;
		public static int BlockIndex(this int index) => RowIndex(index) / 3 * 3 + ColumnIndex(index) / 3;

		public static Region CreateRow(this Cell[] grid, int rowIndex)
		{
			var row = new Cell[9];

			for (var i = 0; i < 9; i++)
			{
				row[i] = grid[rowIndex * 9 + i];
			}

			return new Region(row);
		}

		public static Region CreateColumn(this Cell[] grid, int columnIndex)
		{
			var column = new Cell[9];

			for (var i = 0; i < 9; i++)
			{
				column[i] = grid[columnIndex + i * 9];
			}

			return new Region(column);
		}

		public static Region CreateBlock(this Cell[] grid, int blockIndex)
		{
			var block = new Cell[9];
			var indicies = Definitions.Block[blockIndex];

			for (var i = 0; i < 9; i++)
			{
				block[i] = grid[indicies[i]];
			}

			return new Region(block);
		}

		public static string ToDisplayString(this Board board)
		{
			var sb = new StringBuilder();

			for (var i = 1; i <= 81; i++)
			{
				var v = board[i - 1].Value;
				var c = v == 0 ? "." : v.ToString();
				sb.Append(c).Append(" ");
				if (i % 9 == 0) sb.AppendLine();
			}

			return sb.ToString();
		}

		public static string ToAnnotationString(this Board board)
		{
			var sb = new StringBuilder();

			for (var i = 0; i < 81; i++)
			{
				sb.Append(board[i].Value);
			}

			return sb.ToString();
		}
	}
}