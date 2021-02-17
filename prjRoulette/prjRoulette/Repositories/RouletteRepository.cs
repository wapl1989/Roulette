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

        DBRedisRoulette dBRedis;

        public RouletteRepository(IConnectionMultiplexer _connectionMultiplexer)
        {
            dBRedis = new DBRedisRoulette(_connectionMultiplexer);
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

        public async Task<string> OpenRoulette(string id)
        {            
            if (await dBRedis.OpenRoulette(id))
                return "Exitosa";
            else
                return "Denegada";
        }
    }
}
