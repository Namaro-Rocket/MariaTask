namespace MariaTask.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.Contracts;
	using System.Linq;
	using Models;

	public sealed class TwoHoursMeasurementScheduleListBuilder
	{
		private int DayCount { get; set; }

		private Func<int> TimeIntervalsCountFunc { get; set; }

		private Func<int> EachIntervalMeasurmentsCountFunc { get; set; }

		private IEnumerable<Town> TownList { get; set; }

		private IMeasurementOrderManager OrderManager { get; set; }

		private DateTime DateStartPoint { get; set; } = DateTime.Now;

		public TwoHoursMeasurementScheduleListBuilder WithTotalDays(int count)
		{
			this.DayCount = count;
			return this;
		}

		public TwoHoursMeasurementScheduleListBuilder TimeIntervalsCount(Func<int> timeIntervalsCountFunc)
		{
			this.TimeIntervalsCountFunc = timeIntervalsCountFunc;
			return this;
		}

		public TwoHoursMeasurementScheduleListBuilder EachIntervalMeasurmentsCount(Func<int> eachIntervalMeasurmentsCountFunc)
		{
			this.EachIntervalMeasurmentsCountFunc = eachIntervalMeasurmentsCountFunc;
			return this;
		}

		public TwoHoursMeasurementScheduleListBuilder WithTownList(IEnumerable<Town> townList)
		{
			this.TownList = townList;
			return this;
		}

		public TwoHoursMeasurementScheduleListBuilder WithOrderManager(IMeasurementOrderManager manager)
		{
			this.OrderManager = manager;
			return this;
		}

		public TwoHoursMeasurementScheduleListBuilder WithDateStartPoint(DateTime date)
		{
			this.DateStartPoint = date.Date;
			return this;
		}

		public List<TwoHoursMeasurementSchedule> Build()
		{
			Contract.Requires<ArgumentException>(TimeIntervalsCountFunc != null, "Time interval count function not init in builder. Build is impossible.");
			Contract.Requires<ArgumentException>(EachIntervalMeasurmentsCountFunc != null, "Each measurement interval function not init in builder. Build is impossible.");
			Contract.Requires<ArgumentException>(TownList != null, "Town list not init in builder. Build is impossible.");
			Contract.Requires<ArgumentException>(OrderManager != null, "Order manager not init in builder. Build is impossible.");

			var sheduleList = new List<TwoHoursMeasurementSchedule>();

			foreach (var town in TownList)
			{
				foreach (int day in Enumerable.Range(0, DayCount))
				{
					var n = TimeIntervalsCountFunc();
					foreach (int interval in Enumerable.Range(0, n))
					{
						var m = EachIntervalMeasurmentsCountFunc();
						foreach (int measure in Enumerable.Range(0, m))
						{
							var WorkStartTime = TimeSpan.FromHours(8);
							var TimeOffset = TimeSpan.FromHours(2 * interval);
							var DateOffset = TimeSpan.FromDays(day);
							var curDate = this.DateStartPoint + DateOffset + WorkStartTime + TimeOffset;
							sheduleList.Add(new TwoHoursMeasurementSchedule(this.OrderManager, town, curDate));
						}
					}
				}
			}
			return sheduleList;
		}
	}
}
