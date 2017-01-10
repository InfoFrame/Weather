using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Weather_Core;
using MvvmCross.Binding.BindingContext;
using Weather_Core.ViewModels;

namespace Weather_iOS.Views
{
	public partial class CitiesView : MvxViewController
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

			Title = "Cities";
			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Add", UIBarButtonItemStyle.Plain, null);

			var source = new TableSource(TableView, "CityCell", "Cell");

			this.CreateBinding(source).To<CitiesViewModel>(vm => vm.Todays).Apply();
			this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<CitiesViewModel>(vm => vm.ShowCityCommand).Apply();
			this.CreateBinding(NavigationItem.RightBarButtonItem).To<CitiesViewModel>(vm => vm.AddCityCommand).Apply();
				

		//	this.CreateBinding(source).For(s => s.Addc).To<CitiesViewModel>(vm => vm.AddCityCommand).Apply();

			TableView.Source = source;
			TableView.ReloadData();
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.RefreshCommand.Execute();
		}

	}
}

