
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Syncfusion.Charts;
using MvvmCross.Droid.Views;
using Weather_Core;
using Weather_Core.Models;
using Weather_Core.ViewModels;


namespace Weather_Droid.Views
{
	[Activity(Label = "")]
	public class CityView : MvxActivity
	{

		public class ChartDataModel
		{
			public ObservableArrayList highTemperature;
			public ObservableArrayList lowTemperature;

			Forecast _forecast;

			public ChartDataModel(Forecast forecast)
			{
				_forecast = forecast;
				highTemperature = new ObservableArrayList();
				lowTemperature = new ObservableArrayList();

				foreach (var day in _forecast.Forecasts)
				{
					AddDataPointsForChart(UnixTimeStampToDateTime(day.Day).ToString("M"), day.TempHigh, day.TempLow);
				}
			}

			void AddDataPointsForChart(String day, Double high, Double low)
			{
				highTemperature.Add(new ChartDataPoint(day, high));
				lowTemperature.Add(new ChartDataPoint(day, low));
			}

		}

		public new CityViewModel ViewModel
		{
			get { return (CityViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
			SetContentView(Resource.Layout.City);
			Title = ViewModel.Today.CityName;
		}

		SfChart Chart;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Chart = FindViewById<SfChart>(Resource.Id.Chart);
			CategoryAxis primaryAxis = new CategoryAxis();
			primaryAxis.Title.Text = "Day";
			Chart.PrimaryAxis = primaryAxis;
			NumericalAxis secondaryAxis = new NumericalAxis();
			secondaryAxis.Title.Text = "Temperature";
			Chart.SecondaryAxis = secondaryAxis;

			Chart.Title.Text = "Weather forecast";
			Chart.Legend.Visibility = Com.Syncfusion.Charts.Enums.Visibility.Visible;
			UpdateChart();
			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName.Equals("Forecast"))
				{
					UpdateChart();
				}
			};
		}

		private void UpdateChart()
		{
			if (ViewModel.Forecast != null)
			{
				ChartDataModel dataModel = new ChartDataModel(ViewModel.Forecast);
				Chart.Series.Add(new SplineSeries()
				{
					DataSource = dataModel.highTemperature,
					Color = Color.Red,
					Label = "High"
				});
				Chart.Series.Add(new SplineSeries()
				{
					DataSource = dataModel.lowTemperature,
					Color = Color.Blue,
					Label = "Low"
				});
			}
		}

		private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

	}
}
