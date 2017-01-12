using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using Syncfusion.SfChart.iOS;
using UIKit;
using Weather_Core;
using Weather_Core.Models;
using Weather_Core.ViewModels;

namespace Weather_iOS.Views
{
	public partial class CityView : MvxViewController
	{

		public class TableSource : MvxSimpleTableViewSource
		{

			public TableSource(UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(tableView, nibName, cellIdentifier, bundle)
			{
			}

			public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return 60;
			}

		}



		public class ChartDataModel : SFChartDataSource
		{
			NSMutableArray highTemperature;
			NSMutableArray lowTemperature;
			NSMutableArray precipitation;

			Forecast _forecast;

			public ChartDataModel(Forecast forecast)
			{
				_forecast = forecast;
				highTemperature = new NSMutableArray();
				lowTemperature = new NSMutableArray();
				precipitation = new NSMutableArray();

				foreach (var day in _forecast.Forecasts)
				{
					AddDataPointsForChart(UnixTimeStampToDateTime(day.Day).ToString("M"), day.TempHigh, day.TempLow);
				}
			}

			void AddDataPointsForChart(String day, Double high, Double low)
			{
				highTemperature.Add(new SFChartDataPoint(NSObject.FromObject(day), NSObject.FromObject(high)));
				lowTemperature.Add(new SFChartDataPoint(NSObject.FromObject(day), NSObject.FromObject(low)));
			}

			public override nint GetNumberOfDataPoints(SFChart chart, nint index)
			{
				return _forecast.Forecasts.Count;
			}

			public override SFChartDataPoint GetDataPoint(SFChart chart, nint index, nint seriesIndex)
			{
				if (seriesIndex == 0)
				{
					return highTemperature.GetItem<SFChartDataPoint>((nuint)index);
				}
				else {
					return lowTemperature.GetItem<SFChartDataPoint>((nuint)index);
				}
			}

			public override SFSeries GetSeries(SFChart chart, nint index)
			{
				if (index == 0)
				{
					SFSplineSeries series = new SFSplineSeries();
					series.Label = new NSString("High");
					series.Color = UIColor.Red;
					return series;
				}
				else {
					SFSplineSeries series = new SFSplineSeries();
					series.Label = new NSString("Low");
					series.Color = UIColor.Blue;
					return series;
				}
			}

			public override nint NumberOfSeriesInChart(SFChart chart)
			{
				return 2;
			}

			private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
			{
				// Unix timestamp is seconds past epoch
				System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
				dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
				return dtDateTime;
			}
		}



		public CityView() : base("CityView", null)
		{
		}

		public new CityViewModel ViewModel
		{
			get { return (CityViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.CreateBinding(this).For(a => a.Title).To<CityViewModel>(vm => vm.Today.CityName).Apply();
			this.CreateBinding(TempLabel).To<CityViewModel>(vm => vm.Today.CurrentTemp).Apply();
			this.CreateBinding(WeatherDescriptionLabel).To<CityViewModel>(vm => vm.Today.WeatherDescription).Apply();

			//Adding Primary Axis for the Chart.
			SFCategoryAxis primaryAxis = new SFCategoryAxis();
			primaryAxis.Title.Text = new NSString("Day");
			Chart.PrimaryAxis = primaryAxis;

			//Adding Secondary Axis for the Chart.
			SFNumericalAxis secondaryAxis = new SFNumericalAxis();
			secondaryAxis.Title.Text = new NSString("Temperature");
			Chart.SecondaryAxis = secondaryAxis;

			Chart.Title.Text = new NSString("Weather forecast");
			Chart.Legend.Visible = true;
			UpdateChart();
			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName.Equals("Forecast"))
				{
					UpdateChart();
				}
			};
			/*
			var source = new TableSource(TableView, "ForecastCell", "Cell");

			this.CreateBinding(source).To<CityViewModel>(vm => vm.Forecast.Forecasts).Apply();

			TableView.Source = source;

			var refreshControl = new UIRefreshControl();
			refreshControl.ValueChanged += async (sender, e) =>
			{
				await ViewModel.Refresh();
				refreshControl.EndRefreshing();
			};
			TableView.Add(refreshControl);

			TableView.ReloadData();
			*/
		}

		public void UpdateChart()
		{
			if (ViewModel.Forecast != null)
			{
				ChartDataModel dataModel = new ChartDataModel(ViewModel.Forecast);
				Chart.DataSource = dataModel as SFChartDataSource;
			}
		}


	}
}

