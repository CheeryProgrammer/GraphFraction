using System;

namespace GraphFraction
{
	class Fraction
	{
		public int Numerator { get; private set; }
		public int Denominator { get; private set; }
		public bool IsNegative { get; }

		public double Value
		{
			get
			{
				var val = (double)Numerator / Denominator;
				if (IsNegative)
					return -val;
				return val;
			}
		}

		public Fraction(int numerator, int denominator)
		{
			Numerator = Math.Abs(numerator);
			Denominator = Math.Abs(denominator);
			IsNegative = numerator < 0 ^ denominator < 0;
			ReduceFraction();
		}

		private void ReduceFraction()
		{
			int greater = Math.Max(Numerator, Denominator);
			int less = Math.Min(Numerator, Denominator);
			int gcd = FindGCD(greater, less);
			Numerator /= gcd;
			Denominator /= gcd;
		}

		private int FindGCD(int greater, int less)
		{
			var rest = greater % less;
			if (rest == 0)
				return less;
			return FindGCD(less, rest);
		}

		public override string ToString()
		{
			return string.Format("{0}{1}/{2}", IsNegative ? "-" : string.Empty, Numerator, Denominator);
		}
	}
}
