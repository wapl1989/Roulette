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

        private IOperationsRoulette operations;
        
        public RouletteRepository(IOperationsRoulette _operations)
        {            
            operations = _operations;
        }

        public async Task<RouletteDTO> CreateRoulette()
        {
            RouletteDTO roulette = new RouletteDTO
            {
                Id = Guid.NewGuid().ToString(),
                Condition = Models.Enum.ConditionEnum.Close
            };

            if (await operations.CreateRoulette(roulette))
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
            if (await operations.OpenRoulette(id))
                return "Exitosa";
            else
                return "Denegada";
        }

        public async Task<List<ResultsRouletteDTO>> CloseRoulette(string id)
        {           
            return (await operations.CloseRoulette(id));
        }

        public List<RouletteDTO> AllRoulette()
        {            
            return ((List<RouletteDTO>)operations.ListRoulettes());
        }
    }
}
