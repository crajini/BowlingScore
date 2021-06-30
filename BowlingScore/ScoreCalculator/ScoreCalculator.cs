using BowlingScore.Models;
using BowlingScore.ScoreCalculator.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingScore.ScoreCalculator
{
    public class Score : IScore
    {
        int frame = 0;
        ScoreViewModel scoreViewModel;

        public async Task<ScoreViewModel> CalculateScore(List<int> pins)
        {
            scoreViewModel = new ScoreViewModel();
            frame = 0;
            int previousScore;

            for (int i = 0; i < pins.Count; i += 2)
            {
                previousScore = GetPreviousFrameScore(frame);
                // Neither strike nor spare
                if (i + 1 < pins.Count && pins[i] + pins[i + 1] < 10)
                {
                    scoreViewModel.FrameProgressScores.Add((previousScore + pins[i] + pins[i + 1]).ToString());
                    frame += 1;
                    if (frame >= 10)
                        scoreViewModel.GameCompleted = true;
                    continue;
                }
                //strike
                if (IsStrike(pins[i]))
                {
                    if (i + 2 >= pins.Count)
                    {
                        scoreViewModel.FrameProgressScores.Add("*");
                        continue;
                    }
                    scoreViewModel.FrameProgressScores.Add((previousScore + pins[i] + StrikeBonus(pins[i + 1], pins[i + 2])).ToString());
                    frame += 1;
                }
                //spare
                if (i + 1 < pins.Count && IsSpare(pins[i], pins[i + 1]))
                {
                    if (i + 2 >= pins.Count)
                    {
                        scoreViewModel.FrameProgressScores.Add("*");
                        break;
                    }
                    scoreViewModel.FrameProgressScores.Add((previousScore + pins[i] + pins[i + 1] + SpareBonus(pins[i + 2])).ToString());
                    frame += 1;
                }

                // In case of strike, advance only by one
                if (pins[i] == 10)
                    i--;
                if (frame == 10)
                {
                    scoreViewModel.GameCompleted = true;
                    break;
                }
            }

            return await Task.FromResult(scoreViewModel);
        }
        private bool IsStrike(int roll)
        {
            if (roll == 10)
                return true;
            return false;
        }

        private int StrikeBonus(int roll1, int roll2)
        {
            return roll1 + roll2;
        }

        private bool IsSpare(int roll, int roll2)
        {
            if (roll + roll2 == 10)
                return true;
            return false;
        }

        private int SpareBonus(int roll1)
        {
            return roll1;
        }

        private int GetPreviousFrameScore(int frameIndex)
        {
            int score;
            if (frameIndex < 1)
                return 0;
            int.TryParse(scoreViewModel.FrameProgressScores[frame - 1], out score);            
            return score;
        }
    }
}
