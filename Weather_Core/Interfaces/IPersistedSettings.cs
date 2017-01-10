using System;
using System.Collections.Generic;

namespace Weather_Core.Interfaces
{
	public interface IPersistedSettings
	{

		void SetCityIds(IEnumerable<long> cities);
		IEnumerable<long> GetCityIds();
	}
}
