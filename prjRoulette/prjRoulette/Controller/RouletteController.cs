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
        [HttpPost("CreateRoulette")]
        public async Task<IActionResult> CreateRoulette()
        {
            RouletteDTO newRoulette = await roulette.CreateRoulette();
            return Ok(newRoulette.Id);
        }

        [HttpPut("OpenRoulette/{id}")]
        public async Task<IActionResult> OpenRoulette(string id)
        {            
            return Ok(await roulette.OpenRoulette(id));
        }
        [HttpPut("CloseRoulette/{id}")]
        public async Task<IActionResult> CloseRoulette(string id)
        {
            return Ok(await roulette.CloseRoulette(id));
        }
    }
}
