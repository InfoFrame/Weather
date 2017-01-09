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
			SetContentView(Resource.Layout.Cities);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);	
		}

	}
}

