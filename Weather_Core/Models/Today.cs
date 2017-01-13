using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Realms;

namespace Weather_Core.Models
{
	public class Today : RealmObject
	{
		[PrimaryKey]
		public long CityId { get; set; }
		public string CityName { get; set; }
		public string WeatherDescription { get; set; }
		public string IconUrl { get; set; }
		public double CurrentTemp { get; set; }
		public long TimeStamp { get; set; }

		// TODO: wrong place (converter?)
		private const string ImageUrlFormat = "http://openweathermap.org/img/w/{0}.png";

		public string ImageUrl 
		{
			get
			{
				return string.Format(ImageUrlFormat, IconUrl);
			}
		}

		public static Today FromJson(string jsonString) {
			dynamic json = JObject.Parse(jsonString);
			var result = new Today()
			{
				CityId = json["id"].Value,
				CityName = json["name"].Value,
				WeatherDescription = json["weather"][0]["description"].Value,
				IconUrl = json["weather"][0]["icon"].Value,
				CurrentTemp = json["main"]["temp"].Value,
				TimeStamp = DateTime.UtcNow.Ticks
			};
			return result;
		}

	}
}

