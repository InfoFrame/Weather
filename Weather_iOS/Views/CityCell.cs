using System;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using MvvmCross.Binding.BindingContext;
using Weather_Core;
using Weather_Core.Models;

namespace Weather_iOS.Views
{
	public partial class CityCell : MvxTableViewCell
	{
		private MvxImageViewLoader _imageHelper;

		public CityCell(IntPtr handle) : base(handle)
		{
			_imageHelper = new MvxImageViewLoader(() => this.ImageView);
			this.DelayBind(() =>
			{
				var binding = this.CreateBindingSet<CityCell, Today>();
				binding.Bind(Name).To(a => a.CityName);
				binding.Bind(WeatherDescription).To(a => a.WeatherDescription);

				binding.Bind(_imageHelper).To(a => a.IconId).WithConversion("IconIdToUrlConverter");
				binding.Apply();
			});
		}
	}
}
