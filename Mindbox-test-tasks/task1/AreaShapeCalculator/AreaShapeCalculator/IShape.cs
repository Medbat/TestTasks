namespace AreaShapeCalculator
{
	/// <summary>
	/// Abstraction for shape with computable area.
	/// </summary>
	public interface IShape
	{
		/// <summary>
		/// Gets the shape area.
		/// </summary>
		double GetShapeArea();
	}
}
