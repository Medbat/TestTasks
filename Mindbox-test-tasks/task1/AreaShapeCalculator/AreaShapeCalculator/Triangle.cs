using System;

namespace AreaShapeCalculator
{
	/// <summary>
	/// Class for describing triangle shape.
	/// </summary>
	public class Triangle : IShape
	{
		/// <summary>
		/// Length of the first side.
		/// </summary>
		public double ALength { get; private set; }

		/// <summary>
		/// Length of the second side.
		/// </summary>
		public double BLength { get; private set; }

		/// <summary>
		/// Length of the third side.
		/// </summary>
		public double CLength { get; private set; }

		/// <summary>
		/// Indicates whether the triangle is right.
		/// </summary>
		public bool IsTriangleRight
		{
			get
			{
				double a, b, c;
				if (ALength > BLength && ALength > CLength)
				{
					c = ALength; b = BLength; a = CLength;
				}
				else
				if (BLength > ALength && BLength > CLength)
				{
					c = BLength; b = ALength; a = CLength;
				}
				else
				{
					c = CLength; b = ALength; a = BLength;
				}
				return a * a + b * b == c * c;
			}
		}

		/// <summary>
		/// Creates a new instance of triangle shape.
		/// </summary>
		/// <param name="aLength">Length of the first side.</param>
		/// <param name="bLength">Length of the second side.</param>
		/// <param name="cLength">Length of the third side.</param>
		/// <exception cref="ArgumentException">Thrown when one of sides' length is less than zero
		/// -OR-
		/// sum of any two sides' length less than third side's length.</exception>
		public Triangle(double aLength, double bLength, double cLength)
		{
			if (aLength <= 0 || bLength <= 0 || cLength <= 0)
			{
				throw new ArgumentException("All trianlge sides' length should be more than 0");
			}
			if (aLength + bLength < cLength || aLength + cLength < bLength || bLength + cLength < aLength)
			{
				throw new ArgumentException("The sum of two any sides's length must exceed the length of third side");
			}
			ALength = aLength;
			BLength = bLength;
			CLength = cLength;
		}

		/// <summary>
		/// Gets the triangle's area.
		/// </summary>
		public double GetShapeArea()
		{
			var p = (ALength + BLength + CLength) / 2;
			return Math.Sqrt(p * (p - ALength) * (p - BLength) * (p - CLength));
		}		
	}
}
