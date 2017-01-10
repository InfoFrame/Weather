using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Settings;
using Weather_Core.Interfaces;

namespace Weather_Core.Services
{
	public class PersistedSettings : IPersistedSettings
	{

		private const string CitiIdsKey = "Cities";

		public void SetCityIds(IEnumerable<long> cities)
		{
			var value = string.Join(",", cities);
			CrossSettings.Current.AddOrUpdateValue<string>(CitiIdsKey, value);
		}

		public IEnumerable<long> GetCityIds()
		{
			var citiesString = CrossSettings.Current.GetValueOrDefault<string>(CitiIdsKey, string.Empty);
			if (string.IsNullOrEmpty(citiesString))
			{
				return new List<long>();
			}
			else {
				var result = citiesString.Split(',').Select(n => Convert.ToInt64(n)).ToList();
				return result;
			}
		}

	}
}
