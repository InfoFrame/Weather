using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Weather_Core;
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
		}

	}
}

