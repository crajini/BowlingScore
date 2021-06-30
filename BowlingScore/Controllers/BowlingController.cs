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
        public async Task<ScoreViewModel>  Scores(RollingViewModel model)
        {
            try
            {
                return await _scoreCalculator.CalculateScore(model.PinsDowned);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
