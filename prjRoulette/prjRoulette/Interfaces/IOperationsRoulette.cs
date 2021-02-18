using prjRoulette.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Interfaces
{
    public interface IOperationsRoulette
    {
        Task<bool> CreateRoulette(RouletteDTO _roulette);
        IList<RouletteDTO> ListRoulettes();
        Task<bool> OpenRoulette(string idRoulette);
        Task<List<ResultsRouletteDTO>> CloseRoulette(string idRoulette);
        
    }
}
