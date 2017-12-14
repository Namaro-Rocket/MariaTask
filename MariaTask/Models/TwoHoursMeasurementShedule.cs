namespace MariaTask.Models
{
	using System;
	using System.Diagnostics.Contracts;
	using Entities;

	/// <summary>
	/// Class represent sheduled measurement for two hours intervel.
	/// </summary>
	public sealed class TwoHoursMeasurementSchedule : MeasurementSchedule
	{
		private DateTime dateFrom;

		/// <summary>
		/// Interval from wich date and time meashure sheduled.
		/// </summary>
		public DateTime DateTimeFrom
		{
			get { return dateFrom; }
			private set
			{
				Contract.Requires<ArgumentException>(value.Date == this.Date, "Time interval not belong the same scheduled day.");
				Contract.Requires<ArgumentException>((value + TimeSpan.FromHours(2)).Date == this.Date, "Time interval upper bound goes over sheduled day.");
				dateFrom = value;
			}
		}

		/// <summary>
		/// Interval until wich date and time meashure sheduled.
		/// </summary>
		public DateTime DateTimeUntil
		{
			get { return this.DateTimeFrom + TimeSpan.FromHours(2); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TwoHoursMeasurementShedule"/> class.
		/// </summary>
		/// <param name="dateTime">Shedule begining date and time</param>
		public TwoHoursMeasurementSchedule(IMeasurementOrderManager manager, Town town, DateTime dateTime)
			:base(manager, town, dateTime)
		{
			this.DateTimeFrom = dateTime;
		}

		public override string ToString()
		{
			return $"Town:{this.Town} Date:{this.Date:d} Time:{this.DateTimeFrom:t}-{this.DateTimeUntil:t}";
		}
	}
}
