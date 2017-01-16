
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;
using Weather_Core.ViewModels;

namespace Weather_Droid.Views
{
	[Activity(Label = "Add City")]
	public class AddCityView : MvxActivity
	{
		public new AddCityViewModel ViewModel
		{
			get { return (AddCityViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
			SetContentView(Resource.Layout.add_city);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}


	}

}
