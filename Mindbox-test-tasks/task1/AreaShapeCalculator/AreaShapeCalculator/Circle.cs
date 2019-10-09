using System;

namespace AreaShapeCalculator
{
	/// <summary>
	/// Class for describing circle shape.
	/// </summary>
	public class Circle : IShape
	{
		/// <summary>
		/// Circle's radius.
		/// </summary>
		public double Radius { get; private set; }

		/// <summary>
		/// Creates a new instance of circle shape.
		/// </summary>
		/// <param name="radius">Circle's radius.</param>
		/// <exception cref="ArgumentException">Thrown when circle radius is less than zero.</exception>		
		public Circle(double radius)
		{
			if (radius <= 0)
			{
				throw new ArgumentException("Circle radius must exceed 0", nameof(radius));
			}
			Radius = radius;
		}

		/// <summary>
		/// Gets the circle's area.
		/// </summary>
		public double GetShapeArea()
		{
			return Math.PI * Radius * Radius;
		}
	}
}
