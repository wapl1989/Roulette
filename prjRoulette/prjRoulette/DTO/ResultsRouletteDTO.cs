using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.DTO
{
    public class ResultsRouletteDTO
    {
        public string IdRoulette { get; set; }
        public string IdBet { get; set; }
        public string User { get; set; }
        public decimal EarnedMoney { get; set; }
    }
}
