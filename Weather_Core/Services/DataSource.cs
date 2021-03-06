﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Realms;
using Weather_Core.Interfaces;
using Weather_Core.Models;
using Weather_Core.Helpers;

namespace Weather_Core.Services
{
	public class DataSource : IDataSource
	{
		private Realm _realm;

		public DataSource()
		{
			_realm = Realm.GetInstance();
		}

		public async Task<Today> GetTodayAsync(long id)
		{
			// TODO: caching error
			var result = _realm.All<Today>().FirstOrDefault(d => (d.CityId == id) && (d.TimeStamp < (DateTime.UtcNow.Ticks)));
			//var result = _realm.All<Today>().FirstOrDefault(d => d.CityId == id && DateTimeOffset.UtcNow.CompareTo(d.TimeStamp.AddSeconds(10)) > 0);
			if (result == null)
			{
				var contentString = await GetResponseAsync(string.Format("{0}weather?id={1}&units=metric&appid={2}", Constants.SERVER_URL, id, Constants.APP_ID));
				result = Today.FromJson(contentString);
				_realm.Write(() => { _realm.Add(result, true); });
			}
			return result;
		}

		public async Task<Forecast> GetForecastAsync(long id)
		{
			var result = _realm.All<Forecast>().FirstOrDefault(d => (d.CityId == id) && (d.TimeStamp < (DateTime.UtcNow.Ticks)));
			if (result == null)
			{
				var contentString = await GetResponseAsync(string.Format("{0}forecast?id={1}&units=metric&appid={2}", Constants.SERVER_URL, id, Constants.APP_ID));
				result = Forecast.FromJson(contentString);
				_realm.Write(() => { _realm.Add(result, true); });
			}
			return result;
		}

		private async Task<string> GetResponseAsync(string url)
		{
			using (var client = new HttpClient())
			{
				using (var response = await client.GetAsync(url))
				{
					response.EnsureSuccessStatusCode();
					var contentString = await response.Content.ReadAsStringAsync();
					return contentString;
				}
			}
		}
	}

}
