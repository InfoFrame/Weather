using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Weather_Core.Interfaces;
using Weather_Core.Models;

namespace Weather_Core.ViewModels
{
	public class CityViewModel : MvxViewModel
	{

		private IDataSource _dataSource;
		private IErrorHandler _errorHandler;

		public CityViewModel(IDataSource dataSource, IErrorHandler errorHandler)
		{
			_dataSource = dataSource;
			_errorHandler = errorHandler;
		}

		public void Init(Today today)
		{
			this.Today = today;
			Refresh();
		}

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

		public async Task Refresh()
		{
			try
			{
				Forecast = await _dataSource.GetForecastAsync(Today.CityId);
			}
			catch (Exception ex)
			{
				_errorHandler.HandleError(ex);
			}

		}
	}
}
