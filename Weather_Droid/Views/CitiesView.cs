using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Weather_Core;
using Android.Views;
using Android.Content;
using Android.Graphics;
using System.Net;
using Android.Net;
using System.Threading.Tasks;
using MvvmCross.Droid.Views;
using Weather_Core.ViewModels;
using Acr.UserDialogs;
using MvvmCross.Platform;
using MvvmCross.Binding.Droid.Views;

namespace Weather_Droid.Views
{
	[Activity(Label = "Weather", MainLauncher = true, Icon = "@mipmap/icon")]
	public class CitiesView : MvxActivity
	{

		public new CitiesViewModel ViewModel
		{
			get { return (CitiesViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
			SetContentView(Resource.Layout.cities);// TODO naming convention
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			UserDialogs.Init(this);
			base.OnCreate(savedInstanceState);
			var listView = FindViewById<MvxListView>(Resource.Id.list_view);
			RegisterForContextMenu(listView);
		}
		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
			menu.Add("Delete?");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			ViewModel.DeleteCityCommand.Execute(info.Position);
			return true;
		}

		protected override void OnResume()
		{
			base.OnResume();
			ViewModel.RefreshCommand.Execute(); // TODO: refresh twice on startup
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.add_city_menu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Resource.Id.menu_add_city:
					ViewModel.AddCityCommand.Execute();
					return true;
				default:
					return base.OnOptionsItemSelected(item);
			}
		}

	}
}

