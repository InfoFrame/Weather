﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Realms;

namespace Weather_Core.Models
{
	public class ForecastDay : RealmObject
	{
		public long CityId { get; set; }
		public string WeatherDescription { get; set; }
		public double TempHigh { get; set; }
		public double TempLow { get; set; }
		public string IconUrl { get; set; }
		public long Day { get; set; }


		private const string ImageUrlFormat = "http://openweathermap.org/img/w/{0}.png";

		public string ImageUrl
			get
			{
				return string.Format(ImageUrlFormat, IconUrl);
			}
		}
	
		private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}