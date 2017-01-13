using System;
using System.Threading.Tasks;
using Weather_Core.Models;

namespace Weather_Core.Interfaces
{
	public interface IDataSource
	{
		// TODO: async
		Task<Today> GetToday(long id);
		Task<Forecast> GetForecast(long id);
	}
}
