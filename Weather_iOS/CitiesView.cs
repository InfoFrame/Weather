using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Weather_Core;
using MvvmCross.Binding.BindingContext;

namespace Weather_iOS
{
	public partial class CitiesView : MvxViewController
	{

		public List<long> _cityIds = new List<long> { 2172797, 7284828, 2647123, 716963 };

		public class TableSource : MvxTableViewSource
		{
			private static readonly NSString CellIdentifier = new NSString("Cell");

			public TableSource(UITableView tableView)
				: base(tableView)
			{
				tableView.RegisterNibForCellReuse(UINib.FromName("CityCell", NSBundle.MainBundle), CellIdentifier);
			}

			public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return 60;
			}
			protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, Foundation.NSIndexPath indexPath, object item)
			{
				var cell = (UITableViewCell)TableView.DequeueReusableCell(CellIdentifier);

				return cell;
			}

		}

		public CitiesView() : base("CitiesView", null)
		{
		}

		public new CitiesViewModel ViewModel
		{
			get { return (CitiesViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Weather";

			var source = new TableSource(TableView);

			this.AddBindings(new Dictionary<object, string>
			{
				{source, "ItemsSource Todays"}
			});

			TableView.Source = source;
			TableView.ReloadData();
		}


	}
}

