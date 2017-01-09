using System;
using System.Threading.Tasks;
using Weather_Core.Models;

namespace Weather_Core.Interfaces
{
	public interface IDataSource
	{

		Task<Today> GetToday(long id);
		Task<Forecast> GetForecast(long id);
	}
}
