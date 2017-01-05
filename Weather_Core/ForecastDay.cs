using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Realms;

namespace Weather_Core
{
	public class ForecastDay : RealmObject
	{
		public long CityId { get; set; }
		public string WeatherDescription { get; set; }
		public double TempHigh { get; set; }
		public double TempLow { get; set; }
		public string IconUrl { get; set; }
		public long Day { get; set; }



		private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}
