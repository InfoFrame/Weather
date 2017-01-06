using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using Weather_Core;
using Weather_Core.Models;

namespace Weather_iOS.Views
{
	public partial class ForecastCell : MvxTableViewCell
	{
		private MvxImageViewLoader _imageHelper;

		public ForecastCell(IntPtr handle) : base(handle)
		{
			_imageHelper = new MvxImageViewLoader(() => this.ImageView);
			this.DelayBind(() =>
			{
				var binding = this.CreateBindingSet<ForecastCell, ForecastDay>();
				binding.Bind(DescriptionLabel).To(a => a.WeatherDescription);
				binding.Bind(MaxLabel).To(a => a.TempHigh);
				binding.Bind(MinLabel).To(a => a.TempLow);
				binding.Bind(_imageHelper).To(a => a.ImageUrl);
				binding.Apply();
			});
		}
	}
}
