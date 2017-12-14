namespace MariaTask.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public class NullConverter<T> : IValueConverter
	{
		public NullConverter(T trueValue, T falseValue)
		{
			Null = trueValue;
			Ref = falseValue;
		}

		public T Null { get; set; }
		public T Ref { get; set; }

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? Null : Ref;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
