using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Realms;

namespace Weather_Core.Models
{
	public class ForecastDay : RealmObject
	{
		public string WeatherDescription { get; set; }
		public double TempHigh { get; set; }
		public double TempLow { get; set; }
		public string IconId { get; set; }
		public long Day { get; set; }

		public static ForecastDay FromJson(dynamic item)
		{
			var forecastDay = new ForecastDay
			{
				IconId = item["weather"][0]["icon"].Value,
				WeatherDescription = item["weather"][0]["description"].Value,
				TempHigh = item["main"]["temp_max"].Value,
				TempLow = item["main"]["temp_min"].Value,
				Day = item["dt"].Value
			};
			return forecastDay;
		}
	}
}
