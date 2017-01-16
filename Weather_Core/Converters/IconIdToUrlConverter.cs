using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Weather_Core.Helpers;

namespace Weather_Core.Converters
{
	public class IconIdToUrlConverter: MvxValueConverter<string, string>
	{

		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			var result = string.Format(Constants.ImageUrlFormat, value);
			return result;
		}

		protected override String ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
