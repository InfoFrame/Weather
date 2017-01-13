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
			{ // TODO: refactor deserialization to ForecastDay
				var forecastDay = new ForecastDay
				{
					CityId = json["city"]["id"].Value,
					IconUrl = item["weather"][0]["icon"].Value,
					WeatherDescription = item["weather"][0]["description"].Value,
					TempHigh = item["main"]["temp_max"].Value,
					TempLow = item["main"]["temp_min"].Value,
					Day = item["dt"].Value
				};
				forecast.CityId = json["city"]["id"].Value;
				forecast.Forecasts.Add(forecastDay);
			}
			forecast.TimeStamp = DateTime.UtcNow.Ticks;
			return forecast;
		}
	}
}
