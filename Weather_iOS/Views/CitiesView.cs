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
			CitiesView _citiesView;

			public TableSource(CitiesView citiesView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(citiesView.TableView, nibName, cellIdentifier, bundle)
			{
				_citiesView = citiesView;
			}

			public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				return 60;
			}

			public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete)
				{
					_citiesView.ViewModel.DeleteCityCommand.Execute(indexPath.Row);
				}
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
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Edit", UIBarButtonItemStyle.Plain, null);
			NavigationItem.LeftBarButtonItem.Clicked += (sender, e) =>
			{
				TableView.SetEditing(!TableView.Editing, true);
				NavigationItem.LeftBarButtonItem.Title = TableView.Editing ? "Done" : "Edit";
			};

			var source = new TableSource(this, "CityCell", "Cell");

			this.CreateBinding(source).To<CitiesViewModel>(vm => vm.Todays).Apply();
			this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<CitiesViewModel>(vm => vm.ShowCityCommand).Apply();
			this.CreateBinding(NavigationItem.RightBarButtonItem).To<CitiesViewModel>(vm => vm.AddCityCommand).Apply();
				

		//	this.CreateBinding(source).For(s => s.Addc).To<CitiesViewModel>(vm => vm.AddCityCommand).Apply();

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
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			ViewModel.RefreshCommand.Execute();
		}

	}
}

