using System.Collections.Generic;

namespace ThreadSafeCache.Storages
{
	/// <summary>
	/// ��������� ��� ��������� ����
	/// </summary>
	/// <typeparam name="T">��� �������� ������</typeparam>
	public interface IStorage<T> : IEnumerable<T>
	{
		/// <summary>
		/// ��������� ������� � ���������
		/// </summary>
		/// <param name="element">�������� ��������</param>
		/// <returns>������ ������������ ��������</returns>
		int Add(T element);

		/// <summary>
		/// �������� �������� ������������� �������� �� �������
		/// </summary>
		/// <param name="index">������ �������� ��������</param>
		/// <returns>�������� �������� ��������</returns>
		T Get(int index);
		
		/// <summary>
		/// ������� ������������ ������� �� �������
		/// </summary>
		/// <param name="index">������ ������������� ��������</param>
		void Remove(int index);

		/// <summary>
		/// �������� ���������� �������������� ���������
		/// </summary>
		int Length { get; }
	}
}