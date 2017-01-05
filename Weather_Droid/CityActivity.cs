
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
using Weather_Core;

namespace Weather_Droid
{
	[Activity(Label = "")]
	public class CityActivity : Activity
	{

		private List<ForecastDay> _forecast;

		public class ForecastAdapter : BaseAdapter<ForecastDay>
		{
			Activity context;
			private long _cityId;
			private Forecast _forecasts;


			public ForecastAdapter(Activity context, long cityId) : base()
			{
				this.context = context;
				_cityId = cityId;
				LoadForecast(_cityId);
			}

			public override long GetItemId(int position)
			{
				return position;
			}

			public override ForecastDay this[int position]
			{
				get { return _forecasts.Forecasts[position]; }
			}

			public override int Count
			{
				get { return _forecasts != null ? _forecasts.Forecasts.Count : 0; }
			}


			public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
			{
				View view = convertView; // re-use an existing view, if one is available
				if (view == null) // otherwise create a new one
					view = context.LayoutInflater.Inflate(Resource.Layout.ForecastDay_List_Item, null);

				var forecastDay = _forecasts.Forecasts[position];

				view.FindViewById<TextView>(Resource.Id.name).Text = forecastDay.Day.ToString();
				view.FindViewById<TextView>(Resource.Id.description).Text = forecastDay.WeatherDescription;
				return view;
			}

			private async void LoadForecast(long cityId)
			{
				_forecasts = await DataSource.Instance.GetForecast(cityId);
				NotifyDataSetChanged();
			}

		}

		ListView _listView;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.City);

			// Get our button from the layout resource,
			// and attach an event to it
			_listView = FindViewById<ListView>(Resource.Id.listView);
			var cityId = Intent.GetLongExtra("CityId", 0);
			_listView.Adapter = new ForecastAdapter(this, cityId);
			LoadToday(cityId);

		}

		private async void LoadToday(long cityId)
		{
			var today = await DataSource.Instance.GetToday(cityId);
			Title = today.CityName;
			FindViewById<TextView>(Resource.Id.temp).Text = today.CurrentTemp.ToString();
			FindViewById<TextView>(Resource.Id.description).Text = today.WeatherDescription;
		}
	}
}
