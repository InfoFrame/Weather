using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using Weather_Core.ViewModels;

namespace Weather_iOS
{
	public partial class AddCityView : MvxViewController
	{

		public AddCityView() : base("AddCityView", null)
		{
		}

		public new AddCityViewModel ViewModel
		{
			get { return (AddCityViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Add City";
			var source = new MvxStandardTableViewSource(ResultsTableView, 
			                                            UITableViewCellStyle.Default, 
			                                            new NSString("Cell"), 
			                                            "TitleText Item2");
			this.CreateBinding(source).For(s => s.SelectionChangedCommand).To<AddCityViewModel>(vm => vm.CitySelectedCommand).Apply();
			this.CreateBinding(source).To<AddCityViewModel>(vm => vm.FilteredCities).Apply();
			this.CreateBinding(SearchBar).To<AddCityViewModel>(vm => vm.SearchString).TwoWay().Apply();

			ResultsTableView.Source = source;
			ResultsTableView.ReloadData();

		}

	}
}

