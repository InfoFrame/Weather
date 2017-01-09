
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
using Weather_Core;
using Weather_Core.Models;
using Weather_Core.ViewModels;

namespace Weather_Droid
{
	[Activity(Label = "")]
	public class CityView : MvxActivity
	{
		
		public new CityViewModel ViewModel
		{
			get { return (CityViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();
			SetContentView(Resource.Layout.City);
			Title = ViewModel.Today.CityName;
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

	}
}
