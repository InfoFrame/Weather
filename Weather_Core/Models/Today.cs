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
		public string IconId { get; set; }
		public double CurrentTemp { get; set; }
		public long TimeStamp { get; set; }

		public static Today FromJson(string jsonString) {
			dynamic json = JObject.Parse(jsonString);
			var result = new Today()
			{
				CityId = json["id"].Value,
				CityName = json["name"].Value,
				WeatherDescription = json["weather"][0]["description"].Value,
				IconId = json["weather"][0]["icon"].Value,
				CurrentTemp = json["main"]["temp"].Value,
				TimeStamp = DateTime.UtcNow.Ticks
			};
			return result;
		}

	}
}

