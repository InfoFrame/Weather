using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json.Linq;
using Weather_Core.Interfaces;

namespace Weather_Core.ViewModels
{
	public class AddCityViewModel : MvxViewModel
	{
		private IDataSource _dataSource;

		private IList<string> _allCities;

		private MvxObservableCollection<string> _filteredCities;
		public MvxObservableCollection<string> FilteredCities
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


		public AddCityViewModel(IDataSource dataSource)
		{
			_dataSource = dataSource;
			LoadCities();
			Refresh();
		}

		private MvxCommand<string> _citySelectedCommand;
		public MvxCommand<string> CitySelectedCommand
		{
			get
			{
				_citySelectedCommand = _citySelectedCommand ?? new MvxCommand<string>(CitySelected);
				return _citySelectedCommand;
			}
		}

		private async void Refresh()
		{			
			FilteredCities = new MvxObservableCollection<string>(_allCities.Where(x => x.IndexOf(SearchString, 0, StringComparison.CurrentCultureIgnoreCase) != -1));
		}

		private void LoadCities()
		{
			Assembly assembly = Assembly.Load(new AssemblyName("Weather_Core"));
			string jsonString = GetEmbeddedResourceString(assembly, "city.list.json");
			dynamic json = JObject.Parse(jsonString);
			_allCities = new List<string>();
			foreach (var item in json["list"])
			{
				var cityName = item["name"].Value;
				_allCities.Add(cityName);
			}
		}

		private void CitySelected(string cityName)
		{
		}


		/// <summary>
		/// Attempts to find and return the given resource from within the specified assembly.
		/// </summary>
		/// <returns>The embedded resource stream.</returns>
		/// <param name="assembly"><see cref="Assembly"/> containing embedded resources.</param>
		/// <param name="resource">Full name of embedded resource in <see cref="Assembly"/>.</param>
		public static Stream GetEmbeddedResourceStream(Assembly assembly, string resource)
		{
			var resourceNames = assembly.GetManifestResourceNames();

			var resourcePaths = resourceNames
				.Where(x => x.EndsWith(resource, StringComparison.CurrentCultureIgnoreCase))
				.ToArray();

			if (!resourcePaths.Any())
			{
				throw new Exception(string.Format("Resource ending with {0} not found.", resource));
			}

			if (resourcePaths.Count() > 1)
			{
				throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resource, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
			}

			return assembly.GetManifestResourceStream(resourcePaths.Single());
		}

		/// <summary>
		/// Attempts to find and return the given resource from within the specified assembly.
		/// </summary>
		/// <returns>The embedded resource as a byte array.</returns>
		/// <param name="assembly"><see cref="Assembly"/> containing embedded resources.</param>
		/// <param name="resource">Full name of embedded resource in <see cref="Assembly"/>.</param>
		public static byte[] GetEmbeddedResourceBytes(Assembly assembly, string resource)
		{
			var stream = GetEmbeddedResourceStream(assembly, resource);

			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}

		/// <summary>
		/// Attempts to find and return the given resource from within the specified assembly.
		/// </summary>
		/// <returns>The embedded resource as a string.</returns>
		/// <param name="assembly"><see cref="Assembly"/> containing embedded resources.</param>
		/// <param name="resource">Full name of embedded resource in <see cref="Assembly"/>.</param>
		public static string GetEmbeddedResourceString(Assembly assembly, string resource)
		{
			var stream = GetEmbeddedResourceStream(assembly, resource);

			using (var streamReader = new StreamReader(stream))
			{
				return streamReader.ReadToEnd();
			}
		}

	}
}
