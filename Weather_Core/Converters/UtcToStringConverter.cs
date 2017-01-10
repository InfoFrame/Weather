using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Weather_Core.Converters
{
	public class UtcToStringConverter : MvxValueConverter<long, string>
	{
	
		protected override string Convert(long value, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime date = DateTime.FromFileTimeUtc(value);
			var result = date.ToString("d", culture);
			return result;
		}

		protected override long ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
