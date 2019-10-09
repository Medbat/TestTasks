using AreaShapeCalculator;
using System;
using Xunit;

namespace UnitTests
{
	public class CircleTests
	{
		[Fact]
		public void CreateCircleTest()
		{
			Assert.Throws<ArgumentException>(() => new Circle(-1));
			Assert.Throws<ArgumentException>(() => new Circle(0));
			new Circle(123.123);
		}

		[Theory]
		[InlineData(1, Math.PI, 2)]
		[InlineData(123.14, 123.14 * 123.14 * Math.PI, 5)]
		public void GetCircleAreaTest(double radius, double area, int precision)
		{
			IShape triangle = new Circle(radius);
			Assert.Equal(area, triangle.GetShapeArea(), precision);
		}
	}
}
