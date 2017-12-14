namespace MariaTask.Converters
{
	using System;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using System.Windows.Data;
	using ViewModels;

	/// <summary>
	/// Class represents visibility of element for MeasurementSchedule collection.
	/// Return invisible if no measurments in selected date and town.	
	/// </summary>
	class ScheduleCollectionToVisibility : IMultiValueConverter
	{
		public Visibility Visible   { get; set; } = Visibility.Visible;
		public Visibility Collapsed { get; set; } = Visibility.Collapsed;

		public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			//Read values passed through multibinding.
			var scheduleCollection = values[0] as ObservableCollection<MeasurementScheduleViewModel>;
			var selectedTown       = values[1] as TownViewModel;
			var itemDate           = values[2] as DateTime?;

			//Check expected parametres.
			if (scheduleCollection == null)
				throw new ArgumentException("Input values[0] not ObservableCollection<MeasurementScheduleViewModel>.");

			if (selectedTown == null)
				throw new ArgumentException("Input values[1] not TownViewModel.");

			if (itemDate == null)
				throw new ArgumentException("Input values[2] not DateTime.");

			//Filtrate collection for needed values.
			var filteredCollection = scheduleCollection.Where(s =>
				(s.Date == itemDate) && (s.Town == selectedTown.Model));
			var totalMeasurments = filteredCollection.Count();

			return totalMeasurments == 0 ? Collapsed : Visible;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Cannot convert back");
		}
	}
}
