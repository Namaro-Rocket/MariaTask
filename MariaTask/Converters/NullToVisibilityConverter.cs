namespace MariaTask.Converters
{
	using System.Windows;

	public sealed class NullToVisibilityConverter : NullConverter<Visibility>
	{
		public NullToVisibilityConverter() :
			 base(Visibility.Visible, Visibility.Collapsed)
		{

		}
	}
}
