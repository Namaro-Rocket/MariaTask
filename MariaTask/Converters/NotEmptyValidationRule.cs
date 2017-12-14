namespace MariaTask.Converters
{
	using System.Windows.Controls;
	using System.Globalization;

	public sealed class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "Необходимо заполнить поле.")
				: ValidationResult.ValidResult;
		}
	}
}
