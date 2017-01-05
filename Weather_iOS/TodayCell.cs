using System;

using Foundation;
using UIKit;
using Weather_Core;

namespace Weather_iOS
{
	public partial class TodayCell : UITableViewCell
	{
		public const string CellIdentifier = "CityCell";
		public const float CellHeight = 60;

		private const string ImageUrlFormat = "http://openweathermap.org/img/w/{0}.png";

		private long _cityId;


		public TodayCell(long cityId) : base(UITableViewCellStyle.Default, CellIdentifier)
		{
			_cityId = cityId;
			NSBundle.MainBundle.LoadNib("TodayCell", this, null);
			var frame = this.Frame;
			frame.Height = CellHeight;
			this.Frame = frame;
			ContainerView.Frame = this.Bounds;
			this.AddSubview(ContainerView);
			LoadToday();
		}

		private async void LoadToday()
		{
			var today = await DataSource.Instance.GetToday(_cityId);
			NameLabel.Text = today.CityName;
			DescriptionLabel.Text = today.WeatherDescription;

			using (var url = new NSUrl(string.Format(ImageUrlFormat, today.IconUrl)))
			{
				using (var data = NSData.FromUrl(url))
				{
					if (data != null)
					{
						Image.Image = UIImage.LoadFromData(data);
					}
				}
			}
		}


	}
}
