using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Realms;

namespace Weather_Core.Models
{
	public class Forecast : RealmObject
	{
		[PrimaryKey]
		public long CityId { get; set; }
		public long TimeStamp { get; set; }
		public IList<ForecastDay> Forecasts { get; }

		public static Forecast FromJson(string jsonString)
		{
			dynamic json = JObject.Parse(jsonString);
			Forecast forecast = new Forecast();
			foreach (var item in json["list"])
			{
				var forecastDay = ForecastDay.FromJson(item);
				forecast.Forecasts.Add(forecastDay);
			}
			forecast.CityId = json["city"]["id"].Value;
			forecast.TimeStamp = DateTime.UtcNow.Ticks;
			return forecast;
		}
	}
}
