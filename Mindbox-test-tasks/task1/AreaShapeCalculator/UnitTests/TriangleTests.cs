using AreaShapeCalculator;
using System;
using Xunit;

namespace UnitTests
{
	public class TriangleTests
	{
		[Fact]
		public void CreateTriangleTest()
		{
			Assert.Throws<ArgumentException>(() => new Triangle(-1, 1, 1));
			Assert.Throws<ArgumentException>(() => new Triangle(1, 0, 1));
			Assert.Throws<ArgumentException>(() => new Triangle(1, 1, 0));
			Assert.Throws<ArgumentException>(() => new Triangle(1, 2, 5));
			new Triangle(1, 2, 3);
		}

		[Theory]
		[InlineData(13, 41, 52, 159.499, 3)]
		[InlineData(3, 4, 5, 6, 15)]
		public void GetTriangleAreaTest(double a, double b, double c, double area, int precision)
		{
			IShape triangle = new Triangle(a, b, c);
			Assert.Equal(area, triangle.GetShapeArea(), precision);
		}

		[Theory]
		[InlineData(3, 4, 5, true)]
		[InlineData(1, 2, 3, false)]
		public void IsTriangleRightTest(double a, double b, double c, bool isRight)
		{
			Triangle triangle = new Triangle(a, b, c);
			Assert.Equal(isRight, triangle.IsTriangleRight);
		}
	}
}
