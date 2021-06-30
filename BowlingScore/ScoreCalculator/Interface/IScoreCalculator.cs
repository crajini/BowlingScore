
using BowlingScore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingScore.ScoreCalculator.Interface
{
    public interface IScore
    {
        Task<ScoreViewModel> CalculateScore(List<int> pinsDowned);
        Task<bool> ValidateInput(List<int> pinsDowned);
    }
}
