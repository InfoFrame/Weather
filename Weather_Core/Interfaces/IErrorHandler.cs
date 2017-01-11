using System;
namespace Weather_Core.Interfaces
{
	public interface IErrorHandler
	{
		void HandleError(Exception ex);
	}

}
