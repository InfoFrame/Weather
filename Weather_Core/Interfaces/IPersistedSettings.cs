using System;
using System.Collections.Generic;

namespace Weather_Core.Interfaces
{
	public interface IPersistedSettings
	{
		IEnumerable<long> CityIds { get; set;}
	}
}
