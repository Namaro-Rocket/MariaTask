namespace MariaTask.Models
{
	using System;
	using System.Linq;
	using System.Diagnostics.Contracts;
	using Entities;

	/// <summary>
	/// Class represent abstract sheduled measure.
	/// </summary>
	public abstract class MeasurementSchedule
	{
		protected Town town;
		protected DateTime date;
		protected MeasurementOrder order;
		protected bool orderWasAssigned;
		protected IMeasurementOrderManager orderManager;

		/// <summary>
		/// Target town where measure can be done.
		/// </summary>
		public Town Town
		{
			get { return town; }
			protected set
			{
				Contract.Requires<NullReferenceException>(value != null, "Town param can't be null");
				town = value;
			}
		}

		/// <summary>
		/// Date of scheduled measure.
		/// </summary>
		public DateTime Date
		{
			get { return date; }
			protected set	{ date = value.Date; }
		}

		/// <summary>
		/// Order assign status.
		/// </summary>
		public MeasurmentScheduleStatus OrderStatus
		{
			get
			{
				if (orderWasAssigned)
				{
					if (this.Order == null)
					{
						return MeasurmentScheduleStatus.Canceled;
					}
					else return MeasurmentScheduleStatus.Assigned;
				}
				else return MeasurmentScheduleStatus.Unassigned;
			}
		}

		/// <summary>
		/// Measurement order information.
		/// </summary>
		public MeasurementOrder Order
		{
			get { return order; }
			set
			{
				Contract.Requires<NullReferenceException>(this.OrderManager != null, "Measurement order manager not prepeared.");
				Contract.Requires<ArgumentException>((value == null) || this.OrderManager.Storage.Contains(value), "Current order manager doesn't contains this order.");
				this.orderWasAssigned = true;
				order = value;
			}
		}

		/// <summary>
		/// Measure information
		/// </summary>
		public IMeasurementOrderManager OrderManager
		{
			get { return orderManager; }
			set
			{
				Contract.Requires<NullReferenceException>(value != null, "Measurement order manager can't be null.");
				orderManager = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MeasurementSchedule"/> class.
		/// </summary>
		/// <param name="dateTime">Measurement date</param>
		public MeasurementSchedule(IMeasurementOrderManager manager, Town town, DateTime date)
		{
			this.orderWasAssigned = false;
			this.OrderManager = manager;
			this.Town = town;
			this.Date = date;
		}
	}
}
