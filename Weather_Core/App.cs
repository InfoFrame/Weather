using System;
using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Weather_Core.Interfaces;
using Weather_Core.Services;
using Weather_Core.ViewModels;

namespace Weather_Core
{
	public class App : MvxApplication
	{
		public App()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<CitiesViewModel>());
			Mvx.RegisterSingleton<IPersistedSettings>(() => new PersistedSettings());
			Mvx.RegisterType<IErrorHandler, ErrorHandler>();
			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
			Mvx.RegisterType<IDataSource, DataSource>();
		}
	}
}
