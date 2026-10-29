namespace DotNetLab.Sudoku;

public class Puzzle
{
    public string Solution { get; set; } = "";
    public int this[int index] => Values[index];

    private int[] Values { get; }

    public Puzzle(string str)
    {
        if (str.Length != 81)
        {
            throw new ArgumentException($"Puzzle string must be exactly 81 characters, got {str.Length}.", nameof(str));
        }

        var values = new int[81];

        for (var i = 0; i < 81; i++)
        {
            if (str[i] < '0' || str[i] > '9')
            {
                throw new ArgumentException($"Invalid character '{str[i]}' at position {i}.", nameof(str));
            }

            values[i] = str[i] - '0';
        }

        Values = values;
    }
}
