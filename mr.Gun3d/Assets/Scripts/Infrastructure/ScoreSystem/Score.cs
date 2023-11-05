using System;
using Infrastructure.DataSavingSystem;

namespace Infrastructure.ScoreSystem
{
    public class Score : IScore
    {
        private readonly ISaveLoadService _saveLoadService;

        public Score(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public int CurrentLevelScore { get; private set; }
        public event Action<int> OnScoreChanged;
        

        public void AddScore(int score)
        {
            CurrentLevelScore += score;
            OnScoreChanged?.Invoke(CurrentLevelScore);
        }

        public void ClearScore()
        {
            CurrentLevelScore = 0;
        }

        public void SaveScore()
        {
            _saveLoadService.SaveScore(CurrentLevelScore);
        }
        
    }
}