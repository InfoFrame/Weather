using Foundation;
using System;
using UIKit;
using System.Collections;
using System.Collections.Generic;

namespace Weather_iOS
{
	public partial class CitiesViewController : UIViewController
	{

//		public List<long> _cityIds = new List<long> { 2172797 };
		public List<long> _cityIds = new List<long> { 2172797, 7284828, 2647123, 716963 };

		public class TableSource : UITableViewSource
		{

			private List<long> _cityIds;
			private CitiesViewController _citiesViewController;

			public TableSource(CitiesViewController citiesViewController, List<long> cityIds)
			{
				_citiesViewController = citiesViewController;
				_cityIds = cityIds;
			}

			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return _cityIds.Count;
			}
		
			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return TodayCell.CellHeight;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell(TodayCell.CellIdentifier) ?? new TodayCell(_cityIds[indexPath.Row]);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				_citiesViewController.PerformSegue("showCity", new NSNumber(_cityIds[indexPath.Row]));

//				_citiesViewController.NavigationController.PushViewController(new CityViewController(_cityIds[indexPath.Row]), true); 
			}
		}

		public CitiesViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.Source = new TableSource(this, _cityIds);

		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);
			if (segue.Identifier.Equals("showCity")) {
				var cityViewController = segue.DestinationViewController as CityViewController;
				cityViewController._cityId = (sender as NSNumber).LongValue;
			}
		}

	}
}