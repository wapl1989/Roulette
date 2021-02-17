using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.DTO
{
    public class ResultsRouletteDTO
    {
        public int IdRoulette { get; set; }
        public int IdBet { get; set; }
        public string User { get; set; }
        public decimal EarnedMoney { get; set; }
    }
}
