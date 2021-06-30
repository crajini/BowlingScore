using System.Collections.Generic;

namespace BowlingScore.Models
{
    public class ScoreViewModel
    {
        public ScoreViewModel()
        {
            FrameProgressScores = new List<string>();
        }
        public List<string> FrameProgressScores { get; set; }
        public bool GameCompleted { get; set; }
    }
}
