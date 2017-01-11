using System;
using Acr.UserDialogs;
using MvvmCross.Platform;
using Weather_Core.Interfaces;

namespace Weather_Core.Services
{
	public class ErrorHandler : IErrorHandler
	{
		public void HandleError(Exception ex)
		{
			Mvx.Resolve<IUserDialogs>().Alert("Error", ex.Message);
		}
	}
}
