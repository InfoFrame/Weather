using System;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using Weather_Core;
using Weather_Core.Converters;
using MvvmCross.Platform.Converters;
namespace Weather_Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext)
			: base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			base.FillValueConverters(registry);
			registry.AddOrOverwrite("UtcToStringConverter", new UtcToStringConverter());
			registry.AddOrOverwrite("IconIdToUrlConverter", new IconIdToUrlConverter());
		}

	}
}
