using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Weather_Core.Interfaces;
using Weather_Core.Models;

namespace Weather_Core.ViewModels
{
	public class CitiesViewModel : MvxViewModel
	{

		private IDataSource _dataSource;
		private IPersistedSettings _persistedSettings;
		private IErrorHandler _errorHandler;

		public CitiesViewModel(IDataSource dataSource, IPersistedSettings persistedSettings, IErrorHandler errorHandler)
		{
			_dataSource = dataSource;
			_persistedSettings = persistedSettings;
			_errorHandler = errorHandler;
		}

		public void Init()
		{
			RefreshCommand.Execute();
		}

		private MvxObservableCollection<Today> _todays;
		public MvxObservableCollection<Today> Todays
		{
			get { return _todays; }
			set { _todays = value; RaisePropertyChanged(() => Todays); }
		}

		private MvxCommand _refreshCommand;
		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(() => Refresh());
				return _refreshCommand;
			}
		}

		private MvxCommand<Today> _showCityCommand;
		public MvxCommand<Today> ShowCityCommand
		{
			get
			{
				_showCityCommand = _showCityCommand ?? new MvxCommand<Today>(ShowCity);
				return _showCityCommand;
			}
		}

		private MvxCommand _addCityCommand;
		public MvxCommand AddCityCommand
		{
			get
			{
				_addCityCommand = _addCityCommand ?? new MvxCommand(AddCity);
				return _addCityCommand;
			}
		}

		private MvxCommand<int> _deleteCityCommand;
		public MvxCommand<int> DeleteCityCommand
		{
			get
			{
				_deleteCityCommand = _deleteCityCommand ?? new MvxCommand<int>(x => DeleteCity(x));
				return _deleteCityCommand;
			}
		}

		private void ShowCity(Today today)
		{
			ShowViewModel<CityViewModel>(today);

		}

		private void AddCity()
		{
			ShowViewModel<AddCityViewModel>();
		}

		private void DeleteCity(int index)
		{
			var cityIds = _persistedSettings.GetCityIds();
			var newCityIds = cityIds.ToList();
			newCityIds.RemoveAt(index);
			_persistedSettings.SetCityIds(newCityIds);
			Refresh();
		}

		public async Task Refresh()
		{
			try
			{
				Todays = new MvxObservableCollection<Today>();
				var cityIds = _persistedSettings.GetCityIds();
				foreach (var cityId in cityIds)
				{
					var today = await _dataSource.GetTodayAsync(cityId);
					Todays.Add(today);
				}
			}
			catch (Exception ex)
			{
				_errorHandler.HandleError(ex);
			}
		}

	}
}
