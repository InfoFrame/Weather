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

namespace Weather_Droid
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

		/*
		public class CitiesAdapter : BaseAdapter<long>
		{
			Activity context;
			private List<long> _cityIds;

			public CitiesAdapter(Activity context, List<long> cityIds) : base()
			{
				this.context = context;
				_cityIds = cityIds;
			}

			public override long GetItemId(int position)
			{
				return position;
			}

			public override long this[int position]
			{
				get { return _cityIds[position]; }
			}

			public override int Count
			{
				get { return _cityIds.Count; }
			}


			public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
			{
				View view = convertView; // re-use an existing view, if one is available
				if (view == null) // otherwise create a new one
					view = context.LayoutInflater.Inflate(Resource.Layout.Today_List_Item, null);

				var t = LoadCity(view, position);
				Task.WaitAll(t);
				return view;
			}

			private const string ImageUrlFormat = "http://openweathermap.org/img/w/{0}.png";

			private async Task LoadCity(View view, int position)
			{
				Today today = await DataSource.Instance.GetToday(_cityIds[position]);
				view.FindViewById<TextView>(Resource.Id.name).Text = today.CityName;
				view.FindViewById<TextView>(Resource.Id.description).Text = today.WeatherDescription;

//				var imageBitmap = GetImageBitmapFromUrl(string.Format(ImageUrlFormat, today.IconUrl));
//				view.FindViewById<ImageView>(Resource.Id.icon).SetImageURI(Uri.Parse("https://www.google.hu/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png"));

				view.FindViewById<ImageView>(Resource.Id.icon).SetImageURI(Uri.Parse(string.Format(ImageUrlFormat, today.IconUrl)));
				view.FindViewById<ImageView>(Resource.Id.icon).Invalidate();
			}
			/*
			private Bitmap GetImageBitmapFromUrl(string url)
			{
				Bitmap imageBitmap = null;

				using (var webClient = new WebClient())
				{
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0)
					{
						imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
					}
				}

				return imageBitmap;
			}

	}
*/


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		/*
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Cities);

			// Get our button from the layout resource,
			// and attach an event to it
			_listView = FindViewById<ListView>(Resource.Id.listView);
			_listView.Adapter = new CitiesAdapter(this, _cityIds);

			_listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				var intent = new Intent(this, typeof(CityActivity));
				intent.PutExtra("CityId", (long)_listView.GetItemAtPosition(e.Position));
				StartActivity(intent);
			};
		*/
		}
	}
}

