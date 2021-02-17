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
    public class RouletteRepository : IRoulette
    {

        DBRedis dBRedis;

        public RouletteRepository(IConnectionMultiplexer _connectionMultiplexer)
        {
            dBRedis = new DBRedis(_connectionMultiplexer);
        }

        public async Task<RouletteDTO> CreateRoulette()
        {
            RouletteDTO roulette = new RouletteDTO
            {
                Id = Guid.NewGuid().ToString(),
                Condition = Models.Enum.ConditionEnum.Close
            };

            if (await dBRedis.CreateRoulette(roulette))
                return roulette;
            else
                return new RouletteDTO
                {
                    Id = "NoVALIDO",
                    Condition = Models.Enum.ConditionEnum.Close
                };
        }
    }
}
