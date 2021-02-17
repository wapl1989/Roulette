using prjRoulette.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Interfaces
{
    public interface IRoulette
    {
        public Task<RouletteDTO> CreateRoulette();
        public Task<string> OpenRoulette(string id);
    }
}
