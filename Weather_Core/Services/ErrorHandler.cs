using System;
using Acr.UserDialogs;
using MvvmCross.Platform;
using Weather_Core.Interfaces;

namespace Weather_Core.Services
{
	public class ErrorHandler : IErrorHandler
	{
		IUserDialogs _userDialogs;
		public ErrorHandler(IUserDialogs userDialogs)
		{
			_userDialogs = userDialogs;
		}

		// TODO: localization
		public void HandleError(Exception ex)
		{
			_userDialogs.Alert("Error", ex.Message);
		}
	}
}
