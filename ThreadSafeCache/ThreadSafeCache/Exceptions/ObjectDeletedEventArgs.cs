using System;

namespace ThreadSafeCache.Exceptions
{
	/// <summary>
	/// ����� ��� �������� ������ �� ��������� �������� ���� �����������
	/// </summary>
	public class ObjectDeletedEventArgs : EventArgs
	{
		public int Index;
	}
}