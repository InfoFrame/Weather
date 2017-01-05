using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using Weather_Core;

namespace Weather_iOS
{
	public partial class CityViewController : UIViewController
	{

		public long _cityId;

		public class TableSource : UITableViewSource
		{

			private long _cityId;
			private Forecast _forecasts;
			private UITableView _tableView;

			public TableSource(UITableView tableView, long cityId)
			{
				_tableView = tableView;
				_cityId = cityId;
				LoadForecast();
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return _forecasts != null ? _forecasts.Forecasts.Count : 0;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return ForecastDayCell.CellHeight;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell(ForecastDayCell.CellIdentifier) ?? new ForecastDayCell(_forecasts.Forecasts[indexPath.Row]);
				return cell;
			}

			private async void LoadForecast()
			{
				_forecasts = await DataSource.Instance.GetForecast(_cityId);
				_tableView.ReloadData();
			}
		}

		public CityViewController(long cityId) : base("CityViewController", null)
		{
			_cityId = cityId;
		}

		public CityViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.Source = new TableSource(TableView, _cityId);
			LoadToday();
		}

		private async void LoadToday()
		{
			var today = await DataSource.Instance.GetToday(_cityId);
			NavigationItem.Title = today.CityName;
			CurrentTempLabel.Text = today.CurrentTemp.ToString();
			CurrentTempDescriptionLabel.Text = today.WeatherDescription;
		}

	}
}

