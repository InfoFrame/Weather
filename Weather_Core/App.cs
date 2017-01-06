using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Weather_Core
{
	public class App : MvxApplication
	{
		public App()
		{
//			Mvx.RegisterType<ICalculation, Calculation>();
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<CitiesViewModel>());
		}
	}
}
