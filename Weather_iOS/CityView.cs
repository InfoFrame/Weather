using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Weather_Core;
using Weather_Core.ViewModels;

namespace Weather_iOS
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

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.CreateBinding(Title).To<CityViewModel>(vm => vm.Today.CityName).Apply();
			this.CreateBinding(TempLabel).To<CityViewModel>(vm => vm.Today.CurrentTemp).Apply();
			this.CreateBinding(WeatherDescriptionLabel).To<CityViewModel>(vm => vm.Today.WeatherDescription).Apply();

			var source = new TableSource(TableView, "ForecastCell", "Cell");

			this.CreateBinding(source).To<CityViewModel>(vm => vm.Forecast.Forecasts).Apply();

			TableView.Source = source;
			TableView.ReloadData();
		}

	}
}

