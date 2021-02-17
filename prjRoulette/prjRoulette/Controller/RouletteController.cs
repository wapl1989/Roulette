using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjRoulette.DTO;
using prjRoulette.Interfaces;

namespace prjRoulette.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private IRoulette roulette;
        public RouletteController(IRoulette _roulette)
        {
            this.roulette = _roulette;
        }
        [HttpPost]
        public IActionResult CreateRoulette()
        {
            RouletteDTO newRoulette = roulette.CreateRoulette();
            return Ok(newRoulette.Id);
        }
    }
}
