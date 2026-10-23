namespace Sudoku;

public sealed record Bitmask
{
    public const int AllValuesMask = 1022;
    public static Bitmask All => new(AllValuesMask);

    public int Value { get; private set; }

    public Bitmask()
    {
    }

    private Bitmask(int value) => Value = value;

    public void Clear() => Value = 0;

    public void SetValue(int value) => Value |= 1 << value;
    public void SetMask(int mask) => Value |= mask;
    public void Remove(int value) => Value &= ~(1 << value);
    public void RemoveMask(int mask) => Value &= ~mask;
    public void Intersect(int mask) => Value &= mask;
    public bool Contains(int value) => (Value >> value & 1) == 1;

    public IEnumerable<int> Values
    {
        get
        {
            for (var i = 1; i <= 9; i++)
            {
                if (Contains(i))
                {
                    yield return i;
                }
            }
        }
    }

    public int Count => System.Numerics.BitOperations.PopCount((uint)Value);

    public static Bitmask operator &(Bitmask a, Bitmask b) => new(a.Value & b.Value);
}
