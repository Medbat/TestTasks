using System.Collections.Generic;

namespace ThreadSafeCache.Storages
{
	/// <summary>
	/// Интерфейс для хранилища кэша
	/// </summary>
	/// <typeparam name="T">Тип хранимых данных</typeparam>
	public interface IStorage<T> : IEnumerable<T>
	{
		/// <summary>
		/// Добавляет элемент в хранилище
		/// </summary>
		/// <param name="element">Значение элемента</param>
		/// <returns>Индекс добавленного элемента</returns>
		int Add(T element);

		/// <summary>
		/// Получает значение кэшированного элемента по индексу
		/// </summary>
		/// <param name="index">Индекс искомого элемента</param>
		/// <returns>Значение искомого элемента</returns>
		T Get(int index);
		
		/// <summary>
		/// Удаляет кэшированный элемент по индексу
		/// </summary>
		/// <param name="index">Индекс кэшированного элемента</param>
		void Remove(int index);

		/// <summary>
		/// Получает количество закешированных элементов
		/// </summary>
		int Length { get; }
	}
}