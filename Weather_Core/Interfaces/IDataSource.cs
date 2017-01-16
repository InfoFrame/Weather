using System;
using System.Threading.Tasks;
using Weather_Core.Models;

namespace Weather_Core.Interfaces
{
	public interface IDataSource
	{
		Task<Today> GetTodayAsync(long id);
		Task<Forecast> GetForecastAsync(long id);
	}
}
