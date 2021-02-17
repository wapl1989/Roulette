using prjRoulette.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.DTO
{
    public class BetDTO
    {
        [Key]
        public string Id { get; set; }
        [Range(0,36)]
        public int NumberBet { get; set; }
        public BetTypes TypeBet { get; set; }
        [Range(1,10000)]
        [Required]
        public decimal ValueBet { get; set; }
        [Required]
        public string IdRoulette { get; set; }     
        public string User { get; set; }
    }
}
