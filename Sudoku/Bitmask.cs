namespace Sudoku
{
	public class Bitmask
	{
		public static Bitmask All => new(1022);

		public int Value { get; private set; }

		public void Clear() => Value = 0;

		public void SetValues(IEnumerable<int> values)
		{
			foreach (var value in values)
			{
				Value |= 1 << value;
			}
		}

		public Bitmask SetValue(int value)
		{
			Value |= 1 << value;
			return this;
		}

		public void Remove(int value) => Value &= ~(1 << value);
		public bool Contains(int value) => ((Value >> value) & 1) == 1;

		// public bool ContainsAll(params int[] values)
		// {
		// 	// This method can be optimized by using bit operations instead
		// 	foreach (var value in values)
		// 	{
		// 		if (!Contains(value)) return false;
		// 	}
		//
		// 	return true;
		// }

		public IEnumerable<int> Values
		{
			get
			{
				for (var i = 1; i <= 9; i++)
				{
					if (Contains(i)) yield return i;
				}
			}
		}

		public int Count
		{
			get
			{
				if (Value <= 1) return 0;
				var i = Value;
				i -= (i >> 1) & 0x55555555;
				i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
				return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
			}
		}

		public Bitmask()
		{
		}

		private Bitmask(int value)
		{
			Value = value;
		}

		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals(object? obj) => Equals(obj as Bitmask);
		private bool Equals(Bitmask? b) => Value == b!.Value;

		public static bool operator !=(Bitmask a, Bitmask b) => !(a == b);
		public static bool operator ==(Bitmask a, Bitmask b) => a.Value == b.Value;

		public static Bitmask operator &(Bitmask a, Bitmask b) => new(a.Value & b.Value);
	}
}