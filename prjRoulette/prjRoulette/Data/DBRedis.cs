using Newtonsoft.Json;
using prjRoulette.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Data
{
    public class DBRedis
    {
        private static IConnectionMultiplexer connectionMultiplexer;
        private static string nameRoulette = "TableRoulette";
        public DBRedis(IConnectionMultiplexer _connectionMultiplexer)
        {
            connectionMultiplexer = _connectionMultiplexer;
        }
       
        public async Task<bool> CreateRoulette(RouletteDTO _roulette)
        {
            bool create = true;
            try
            {
                IDatabase db = connectionMultiplexer.GetDatabase();
                RedisKey key = new RedisKey(nameRoulette);
                IList<RouletteDTO> roulettes = ListRoulettes() ?? new List<RouletteDTO>();
                roulettes.Add(_roulette);
                await db.StringSetAsync(key, JsonConvert.SerializeObject(roulettes));
            }
            catch (Exception)
            {
                create = false;
            }            
            return create;
        }

        public IList<RouletteDTO> ListRoulettes()
        {
            IDatabase db = connectionMultiplexer.GetDatabase();
            RedisKey key = new RedisKey(nameRoulette);
            string listJson = db.StringGet(key);
            return listJson != null 
                                ? JsonConvert.DeserializeObject<IList<RouletteDTO>>(listJson) 
                                : null;
        }
    }
}
