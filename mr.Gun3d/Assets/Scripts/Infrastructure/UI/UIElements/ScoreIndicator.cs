using Infrastructure.ScoreSystem;
using TMPro;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class ScoreIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IScore _score;

        public void Construct(IScore score)
        {
            _score = score;
            _text.text = score.CurrentLevelScore.ToString();
            _score.OnScoreChanged += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _text.text = score.ToString();
        }
    }
}