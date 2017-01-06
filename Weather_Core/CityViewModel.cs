﻿using System;
using MvvmCross.Core.ViewModels;

namespace Weather_Core
{
	public class CityViewModel : MvxViewModel
	{

		private Today _today;
		public Today Today
		{
			get { return _today; }
			set { _today = value; RaisePropertyChanged(() => Today); }
		}

		private Forecast _forecast;
		public Forecast Forecast
		{
			get { return _forecast; }
			set { _forecast = value; RaisePropertyChanged(() => Forecast); }
		}

		public void Init(Today today)
		{
			this.Today = today;
			Refresh();
		}

		private async void Refresh()
		{
			Forecast = await DataSource.Instance.GetForecast(Today.CityId);
		}

	}
}
