namespace MariaTask.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics.Contracts;
	using System.Globalization;
	using System.Runtime.CompilerServices;
	using Models;

	/// <summary>
	/// ViewModel for MeasurementSchedule.
	/// </summary>
	public class MeasurementScheduleViewModel : INotifyPropertyChanged
	{
		private TwoHoursMeasurementSchedule schedule;
		private string timeFormat = "t";

		/// <summary>
		/// Param on wich our ViewModel based.
		/// </summary>
		public TwoHoursMeasurementSchedule Model
		{
			get { return schedule; }
			private set
			{
				Contract.Requires<NullReferenceException>(value != null, "Measurement shedule model can't be null");
				schedule = value;
			}
		}

		/// <summary>
		/// Time format displayed for this object.
		/// </summary>
		public string CurrentTimeFormat
		{
			get { return timeFormat; }
			private set
			{
				Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value), "Time format can't be null or empty");
				timeFormat = value;
			}
		}

		/// <summary>
		/// Target town where measure can be done.
		/// </summary>
		public Town Town
		{
			get { return this.Model.Town; }
		}

		/// <summary>
		/// Sheduled Date for measure.
		/// </summary>
		public DateTime Date
		{
			get { return this.Model.Date; }
		}

		/// <summary>
		/// Order to measure.
		/// </summary>
		public MeasurementOrder Order
		{
			get { return this.Model.Order; }
			set
			{
				this.Model.Order = value;
				RaisePropertyChanged();
			}
		}

		/// <summary>
		/// Time interval presentation in DataGrid.
		/// </summary>
		public string TimeIntervalStringPresentation
		{
			get { return $"{this.Model.DateTimeFrom.ToString(this.CurrentTimeFormat, CultureInfo.CurrentCulture)} - {this.Model.DateTimeUntil.ToString(this.CurrentTimeFormat, CultureInfo.CurrentCulture)}"; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MeasureScheduleViewModel"/> class.
		/// </summary>
		/// <param name="sheduledMesure">ScheduledMeasure model</param>
		public MeasurementScheduleViewModel(TwoHoursMeasurementSchedule schedule)
		{
			this.Model = schedule;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MeasureScheduleViewModel"/> class.
		/// </summary>
		/// <param name="sheduledMesure">SheduledMeasure model</param>
		/// <param name="timeFormat">Choose time format to display by this view model</param>
		public MeasurementScheduleViewModel(TwoHoursMeasurementSchedule schedule, string timeFormat) 
			:this(schedule)
		{
			this.CurrentTimeFormat = timeFormat;
		}

		public override string ToString()
		{
			return this.Model.ToString();
		}

		#region INotifyPropertyChanged realization

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			RaisePropertyChanged(propertyName);
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
