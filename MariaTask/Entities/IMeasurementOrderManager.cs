namespace MariaTask.Entities
{
	using System.Collections.Generic;
	using Models;

	/// <summary>
	/// interface to order storage manager.
	/// </summary>
	public interface IMeasurementOrderManager
	{
		/// <summary>
		/// Internal manager storage.
		/// </summary>
		IEnumerable<MeasurementOrder> Storage { get; }

		/// <summary>
		/// Adding measurement order to manager storage.
		/// </summary>
		void AddOrder(MeasurementOrder order);
	}
}
