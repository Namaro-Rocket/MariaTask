namespace MariaTask.Converters
{
	using System;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Windows.Data;
	using ViewModels;

	/// <summary>
	/// Class for string representation of MeasurementSchedule collection.
	/// Return empty out of total measurements in current town and date.	
	/// </summary>
	public class ScheduleCollectionToString : IMultiValueConverter
	{
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
			var totalMeasurments   = filteredCollection.Count();
			var freeMeasurements   = totalMeasurments - filteredCollection.Count(s => s.Order != null);

			return $"{freeMeasurements}/{totalMeasurments}";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Cannot convert back");
		}
	}
}
