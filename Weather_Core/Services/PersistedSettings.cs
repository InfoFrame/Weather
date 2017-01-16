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
		public IEnumerable<long> CityIds
		{
			get
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
			set 
			{
				var ids = string.Join(",", value);
				CrossSettings.Current.AddOrUpdateValue<string>(CitiIdsKey, ids);
			}
		}

	}
}
