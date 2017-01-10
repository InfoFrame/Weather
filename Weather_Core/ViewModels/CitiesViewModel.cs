using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Weather_Core.Interfaces;
using Weather_Core.Models;

namespace Weather_Core.ViewModels
{
	public class CitiesViewModel : MvxViewModel
	{

		private IDataSource _dataSource;
		private IPersistedSettings _persistedSettings;
		public CitiesViewModel(IDataSource dataSource, IPersistedSettings persistedSettings)
		{
			_dataSource = dataSource;
			_persistedSettings = persistedSettings;
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
				_refreshCommand = _refreshCommand ?? new MvxCommand(Refresh);
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

		private void ShowCity(Today today)
		{
			ShowViewModel<CityViewModel>(today);

		}

		private void AddCity()
		{
			ShowViewModel<AddCityViewModel>();
		}

		private async void Refresh()
		{
			Todays = new MvxObservableCollection<Today>();
			var cityIds = _persistedSettings.GetCityIds();
			foreach (var cityId in cityIds)
			{
				var today = await _dataSource.GetToday(cityId);
				Todays.Add(today);
			}

		}

	}
}
