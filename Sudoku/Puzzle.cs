namespace Sudoku;

public class Puzzle
{
    public bool Valid { get; } = true;
    public string Solution { get; set; } = "";
    public bool IsSolved() => Solution.Length == 81 && Solution.Contains('0') == false;
    public int this[int index] => Values[index];

    private int[] Values { get; }
    public string Original { get; }

    public Puzzle(string str)
    {
        Original = str;
        Values = str.Select(x => x - '0').ToArray();
        if (Values.Any(x => x > 9)) Valid = false;
    }
}
