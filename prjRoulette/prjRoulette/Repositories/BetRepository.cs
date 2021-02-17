using prjRoulette.Data;
using prjRoulette.DTO;
using prjRoulette.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Repositories
{
    public class BetRepository : IBet
    {
        DBRedisBet dBRedis;
        public BetRepository(IConnectionMultiplexer _connectionMultiplexer)
        {
            dBRedis = new DBRedisBet(_connectionMultiplexer);
        }
        public async Task<string> CreateBet(BetDTO betDTO)
        {
            betDTO.Id = Guid.NewGuid().ToString();
            return (await dBRedis.CreateBet(betDTO));                
        }
    }
}
