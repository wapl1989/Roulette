using Newtonsoft.Json;
using prjRoulette.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Data
{
    public class DBRedisRoulette
    {
        private static IConnectionMultiplexer connectionMultiplexer;
        private static string nameTable = "TableRoulette";
        public DBRedisRoulette(IConnectionMultiplexer _connectionMultiplexer)
        {
            connectionMultiplexer = _connectionMultiplexer;
        }

        public async Task<bool> CreateRoulette(RouletteDTO _roulette)
        {
            bool create = true;
            try
            {
                IDatabase db = connectionMultiplexer.GetDatabase();
                RedisKey key = new RedisKey(nameTable);
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
            RedisKey key = new RedisKey(nameTable);
            string listJson = db.StringGet(key);
            return listJson != null
                                ? JsonConvert.DeserializeObject<IList<RouletteDTO>>(listJson)
                                : null;
        }

        public async Task<bool> OpenRoulette(string id)
        {
            bool update = true;
            try
            {
                IList<RouletteDTO> roulettes = ListRoulettes();
                IDatabase db = connectionMultiplexer.GetDatabase();
                RedisKey key = new RedisKey(nameTable);
                RouletteDTO roulette = roulettes.FirstOrDefault(roulettes => roulettes.Id == id);
                roulettes[roulettes.IndexOf(roulette)].Condition = Models.Enum.ConditionEnum.Open;
                await db.StringSetAsync(key, JsonConvert.SerializeObject(roulettes));
            }
            catch (Exception)
            {
                update = false;
            }
            return update;
        }

        public async Task<List<ResultsRouletteDTO>> CloseRoulette(string id)
        {
            List<ResultsRouletteDTO> resultList = new List<ResultsRouletteDTO>();

            //IList<RouletteDTO> roulettes = ListRoulettes();
            //IDatabase db = connectionMultiplexer.GetDatabase();
            //RedisKey key = new RedisKey(nameTable);
            //RouletteDTO roulette = roulettes.FirstOrDefault(roulettes => roulettes.Id == id);
            //roulettes[roulettes.IndexOf(roulette)].Condition = Models.Enum.ConditionEnum.Open;
            //await db.StringSetAsync(key, JsonConvert.SerializeObject(roulettes));

            return resultList;
        }
    }
}
