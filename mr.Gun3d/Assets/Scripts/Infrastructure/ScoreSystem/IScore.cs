using System;

namespace Infrastructure.ScoreSystem
{
    public interface IScore
    {
        public int CurrentLevelScore { get; }
        public event Action<int> OnScoreChanged;
        public void AddScore(int score);
        public void ClearScore();
        public void SaveScore();
    }
}