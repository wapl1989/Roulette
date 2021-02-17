using prjRoulette.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.Interfaces
{
    public interface IBet
    {
        public Task<string> CreateBet(BetDTO betDTO);
    }
}
