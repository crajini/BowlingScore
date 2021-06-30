using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingScore.Models;
using BowlingScore.ScoreCalculator.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BowlingScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlingController : ControllerBase
    {
        private readonly IScore _scoreCalculator;

        public BowlingController(IScore scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        // POST api/values
        [HttpPost("scores")]
        public async Task<ActionResult<ScoreViewModel>> Scores(RollingViewModel model)
        {
            try
            {

                if (await _scoreCalculator.ValidateInput(model.PinsDowned))
                    return await _scoreCalculator.CalculateScore(model.PinsDowned);
                return BadRequest("Bad Request");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
