using Newtonsoft.Json;
using prjRoulette.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace prjRoulette.Data
{
    public class OperationsRedisRoulette
    {
        
        private static IConnectionMultiplexer connectionMultiplexer;
        private static string nameTable = "TableRoulette";
        public OperationsRedisRoulette(IConnectionMultiplexer _connectionMultiplexer)
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

        public async Task<bool> OpenRoulette(string idRoulette)
        {
            bool update = true;
            try
            {
                IList<RouletteDTO> roulettes = ListRoulettes();
                IDatabase db = connectionMultiplexer.GetDatabase();
                RedisKey key = new RedisKey(nameTable);
                RouletteDTO roulette = roulettes.FirstOrDefault(roulettes => roulettes.Id == idRoulette);
                roulettes[roulettes.IndexOf(roulette)].Condition = Models.Enum.ConditionEnum.Open;
                await db.StringSetAsync(key, JsonConvert.SerializeObject(roulettes));
            }
            catch (Exception)
            {
                update = false;
            }
            return update;
        }

        public async Task<List<ResultsRouletteDTO>> CloseRoulette(string idRoulette)
        {
            IList<RouletteDTO> roulettes = ListRoulettes();
            IDatabase db = connectionMultiplexer.GetDatabase();
            RedisKey key = new RedisKey(nameTable);
            RouletteDTO roulette = roulettes.FirstOrDefault(roulettes => roulettes.Id == idRoulette);
            roulettes[roulettes.IndexOf(roulette)].Condition = Models.Enum.ConditionEnum.Close;
            await db.StringSetAsync(key, JsonConvert.SerializeObject(roulettes));

            return CalculateResults(idRoulette);
        }

        private List<ResultsRouletteDTO> CalculateResults(string idRoulette)
        {
            List<ResultsRouletteDTO> resultList = new List<ResultsRouletteDTO>();
            OperationsRedisBet redisBet = new OperationsRedisBet(connectionMultiplexer);
            IList<BetDTO> bets = redisBet.ListBets() ?? new List<BetDTO>();
            int winNumber = WinNumber();
            if (bets.Count > 0)
                bets = bets.Where(x => x.IdRoulette == idRoulette).ToList();
            foreach (BetDTO dTO in bets)
            {
                ResultsRouletteDTO results = new ResultsRouletteDTO { 
                    IdRoulette = idRoulette,
                    IdBet = dTO.Id,
                    User = dTO.User
                };
                switch (dTO.TypeBet)
                {
                    case Models.Enum.BetTypes.Color:
                        if ((winNumber % 2 == 0) == (dTO.NumberBet % 2 == 0))
                            results.EarnedMoney = dTO.ValueBet * 1.8M;
                        break;
                    case Models.Enum.BetTypes.Number:
                        if (winNumber == dTO.NumberBet)
                            results.EarnedMoney = dTO.ValueBet * 5;
                        break;
                    default:
                        break;
                }
                resultList.Add(results);
            }
            return resultList;
        }

        private int WinNumber()
        {
            Random random = new Random();
            return random.Next(0, 36);
        }
    }
}
