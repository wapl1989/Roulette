using prjRoulette.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjRoulette.DTO
{
    public class RouletteDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public ConditionEnum Condition { get; set; }
    }
}
