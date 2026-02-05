namespace Sudoku;

public class Puzzle
{
    public string Solution { get; set; } = "";
    public int this[int index] => Values[index];

    private int[] Values { get; }

    public Puzzle(string str)
    {
        var values = new int[str.Length];

        for (var i = 0; i < str.Length; i++)
        {
            values[i] = str[i] - '0';
        }

        Values = values;
    }
}
