using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis.Interfaces
{
	public interface IRedisService
	{
		Task<Tuple<T, RedisServiceResponse>> Get<T>(string key);
		Task Set<T>(string key, T value);
	}
}
