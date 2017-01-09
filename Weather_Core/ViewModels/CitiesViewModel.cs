﻿using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Weather_Core.Models;
using Weather_Core.Services;

namespace Weather_Core.ViewModels
{
	public class CitiesViewModel : MvxViewModel
	{

		public List<long> _cityIds = new List<long> { 2172797, 716963 };

		public CitiesViewModel()
		{
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
				var today = await DataSource.Instance.GetToday(cityId);
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

		private void ShowCity(Today today)
		{
			ShowViewModel<CityViewModel>(today);

		}


	}
}
