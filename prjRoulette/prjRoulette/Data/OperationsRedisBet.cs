using Newtonsoft.Json;
using prjRoulette.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Data
{
    public class OperationsRedisBet
    {
        private static IConnectionMultiplexer connectionMultiplexer;
        private static string nameTable = "TableBet";
        public OperationsRedisBet(IConnectionMultiplexer _connectionMultiplexer)
        {
            connectionMultiplexer = _connectionMultiplexer;
        }

        public async Task<string> CreateBet(BetDTO betDTO)
        {
            string response = "";
            if (ValidateRoulette(betDTO.IdRoulette))
            {
                IDatabase db = connectionMultiplexer.GetDatabase();
                RedisKey key = new RedisKey(nameTable);
                IList<BetDTO> bets = ListBets() ?? new List<BetDTO>();
                bets.Add(betDTO);
                await db.StringSetAsync(key, JsonConvert.SerializeObject(bets));
                response = "OK";
            }
            else
            {
                response = "El ID de la apuesta no existe o la ruleta se encuentra cerrada.";
            }
            
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

        private bool ValidateRoulette(string id)
        {
            bool validate = true;
            OperationsRedisRoulette db = new OperationsRedisRoulette(connectionMultiplexer);
            RouletteDTO roulette = db.ListRoulettes().Where(x => x.Id == id).First();
            if (roulette == null)
                validate = false;
            else
            {
                validate = roulette.Condition == Models.Enum.ConditionEnum.Open;
            }
            return validate;
        }
    }
}
