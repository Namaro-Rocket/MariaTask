namespace MariaTask.Converters
{
	using System;

	public sealed class NullToBooleanConverter : NullConverter<Boolean>
	{
		public NullToBooleanConverter() :
			 base(false, true)
		{

		}
	}
}
