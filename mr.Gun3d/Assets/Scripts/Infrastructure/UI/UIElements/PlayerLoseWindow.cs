using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using IScore = Infrastructure.ScoreSystem.IScore;

namespace Infrastructure.UI.UIElements
{
    public class PlayerLoseWindow : MonoBehaviour
    {
        [SerializeField] private GoToMenuButton _menuButton;
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private ScoreIndicator _scoreIndicator;
        public void Construct(IUIMediator uiMediator,IScore score)
        {
            _scoreIndicator.Construct(score);
            _menuButton.Construct(uiMediator);
            _restartButton.Construct(uiMediator);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}