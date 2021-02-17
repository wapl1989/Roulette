using Newtonsoft.Json;
using prjRoulette.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Data
{
    public class DBRedisBet
    {
        private static IConnectionMultiplexer connectionMultiplexer;
        private static string nameTable = "TableBet";
        public DBRedisBet(IConnectionMultiplexer _connectionMultiplexer)
        {
            connectionMultiplexer = _connectionMultiplexer;
        }

        public async Task<string> CreateBet(BetDTO betDTO)
        {
            string response = "";
            IDatabase db = connectionMultiplexer.GetDatabase();
            RedisKey key = new RedisKey(nameTable);
            IList<BetDTO> bets = ListBets() ?? new List<BetDTO>();
            bets.Add(betDTO);
            await db.StringSetAsync(key, JsonConvert.SerializeObject(bets));

            return response;
        }

        public IList<BetDTO> ListBets()
        {
            IDatabase db = connectionMultiplexer.GetDatabase();
            RedisKey key = new RedisKey(nameTable);
            string listJson = db.StringGet(key);
            return listJson != null
                                ? JsonConvert.DeserializeObject<IList<BetDTO>>(listJson)
                                : null;
        }
    }
}
