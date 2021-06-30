
using System.Collections.Generic;

namespace BowlingScore.Calculator.Interface
{
    public interface IScoreCalculator
    {
        List<string> Scores { get; set; }
        List<string> CalculateScore(List<int> pinsDowned);
    }
}
