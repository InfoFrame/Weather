using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json.Linq;
using Weather_Core.Interfaces;
using Weather_Core.Models;

namespace Weather_Core.ViewModels
{
	public class AddCityViewModel : MvxViewModel
	{
		private IPersistedSettings _persistedSettings;

		private IList<Tuple<long, string>> _allCities;
		private MvxObservableCollection<Tuple<long, string>> _filteredCities;
		public MvxObservableCollection<Tuple<long, string>> FilteredCities
		{
			get { return _filteredCities; }
			set { _filteredCities = value; RaisePropertyChanged(() => FilteredCities); }
		}

		private string _searchString = String.Empty;
		public string SearchString
		{
			get { return _searchString; }
			set { _searchString = value; RaisePropertyChanged(() => SearchString); Refresh(); }
		}


		public AddCityViewModel(IPersistedSettings persistedSettings)
		{
			_persistedSettings = persistedSettings;
		}

		public void Init()
		{
			LoadCities();
			Refresh();
		}

		private MvxCommand<Tuple<long, string>> _citySelectedCommand;
		public MvxCommand<Tuple<long, string>> CitySelectedCommand
		{
			get
			{
				_citySelectedCommand = _citySelectedCommand ?? new MvxCommand<Tuple<long, string>>(CitySelected);
				return _citySelectedCommand;
			}
		}

		private void Refresh()
		{			
			FilteredCities = new MvxObservableCollection<Tuple<long, string>>(_allCities.Where(x => x.Item2.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) != -1));
		}

		private void LoadCities()
		{
			Assembly assembly = Assembly.Load(new AssemblyName("Weather_Core"));
			string jsonString = GetEmbeddedResourceString(assembly, "city.list.json");
			dynamic json = JObject.Parse(jsonString);
			_allCities = new List<Tuple<long, string>>();
			foreach (var item in json["list"])
			{
				long cityId = (long)item["_id"].Value;
				string cityName = item["name"].Value as string;
				_allCities.Add(Tuple.Create(cityId, cityName));
			}
		}

		private async void CitySelected(Tuple<long, string> city)
		{
			var persistedCityIds = _persistedSettings.CityIds.ToList();
			persistedCityIds.Add(city.Item1);
			_persistedSettings.CityIds = persistedCityIds;
			Close(this);
		}

		private static string GetEmbeddedResourceString(Assembly assembly, string resource)
		{
			var resourceNames = assembly.GetManifestResourceNames();
			var resourcePaths = resourceNames.Where(x => x.EndsWith(resource, StringComparison.CurrentCultureIgnoreCase)).ToArray();
			if (!resourcePaths.Any())
			{
				throw new Exception(string.Format("Resource ending with {0} not found.", resource));
			} else if (resourcePaths.Count() > 1)
			{
				throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resource, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
			}
			var stream = assembly.GetManifestResourceStream(resourcePaths.Single());
			using (var streamReader = new StreamReader(stream))
			{
				return streamReader.ReadToEnd();
			}
		}

	}
}
