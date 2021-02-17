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
    public class BetController : ControllerBase
    {
        private IBet bet;

        public BetController(IBet _bet)
        {
            this.bet = _bet;
        }

        [HttpPost("CreateBet")]
        public async Task<IActionResult> CreateBet([FromBody] BetDTO betDto)
        {
            if (Request.Headers.TryGetValue("idUser", out var headerValue))
            {
                betDto.User = headerValue.ToString();
                return Ok(await bet.CreateBet(betDto));
            }
            else
            {
                return Ok("No se encontró el ID del Usuario");                
            }
        }
    }
}
