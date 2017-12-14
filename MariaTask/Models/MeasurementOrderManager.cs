namespace MariaTask.Models
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Diagnostics.Contracts;
	using Entities;

	/// <summary>
	/// Class represent measurement order storage manager.
	/// </summary>
	public class MeasurementOrderManager : IMeasurementOrderManager
	{
		private List<MeasurementOrder> storage = new List<MeasurementOrder>();

		/// <summary>
		/// Internal manager storage.
		/// </summary>
		public IEnumerable<MeasurementOrder> Storage
		{
			get { return storage; }
		}

		/// <summary>
		/// Adding measurement order to manager storage. 
		/// </summary>
		public void AddOrder(MeasurementOrder order)
		{
			Contract.Requires<NullReferenceException>(order != null, "Order can't be null.");
			Contract.Requires<ArgumentException>(!Storage.Contains(order), "This manager already contains this order.");
			Contract.Requires<ArgumentException>(!Storage.Any(o => o.Id == order.Id), "This manager already contains order with same id.");
			Contract.Ensures(storage.Count == (Contract.OldValue(storage.Count) + 1), "Order was not added to manager.");

			storage.Add(order);
		}
	}
}
