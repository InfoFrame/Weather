using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Weather_Core.Interfaces;
using Weather_Core.Models;

namespace Weather_Core.ViewModels
{
	public class CitiesViewModel : MvxViewModel
	{

		public List<long> _cityIds = new List<long> { 2172797, 716963 };

		private IDataSource _dataSource;

		public CitiesViewModel(IDataSource dataSource)
		{
			_dataSource = dataSource;
			Refresh();
		}

		private MvxObservableCollection<Today> _todays;
		public MvxObservableCollection<Today> Todays
		{
			get { return _todays; }
			set { _todays = value; RaisePropertyChanged(() => Todays); }
		}


		private async void Refresh()
		{
			Todays = new MvxObservableCollection<Today>();
			foreach (var cityId in _cityIds)
			{
				var today = await _dataSource.GetToday(cityId);
				Todays.Add(today);
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

	}
}
