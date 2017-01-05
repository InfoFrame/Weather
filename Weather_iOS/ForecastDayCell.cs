using System;

using Foundation;
using UIKit;
using Weather_Core;

namespace Weather_iOS
{
	public partial class ForecastDayCell : UITableViewCell
	{
		public const string CellIdentifier = "ForecastDayCell";
		public const float CellHeight = 60;

		private const string ImageUrlFormat = "http://openweathermap.org/img/w/{0}.png";

		public ForecastDayCell(ForecastDay forecastDay) : base(UITableViewCellStyle.Default, CellIdentifier)
		{
			NSBundle.MainBundle.LoadNib("ForecastDayCell", this, null);
			var frame = this.Frame;
			frame.Height = CellHeight;
			this.Frame = frame;
			ContainerView.Frame = this.Bounds;
			this.AddSubview(ContainerView);

			WeatherDescription.Text = forecastDay.WeatherDescription;
			MaxLabel.Text = forecastDay.TempHigh.ToString();
			MinLabel.Text = forecastDay.TempLow.ToString();
			DateLabel.Text = forecastDay.Day.ToString();
			using (var url = new NSUrl(string.Format(ImageUrlFormat, forecastDay.IconUrl)))
			{
				using (var data = NSData.FromUrl(url))
				{
					if (data != null)
					{
						Icon.Image = UIImage.LoadFromData(data);
					}
				}
			}

		}

	

	}
}
