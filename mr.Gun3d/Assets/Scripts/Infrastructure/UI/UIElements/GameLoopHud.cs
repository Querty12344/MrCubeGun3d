using EntityComponents.EntityFacades;
using EntityComponents.Health;
using Infrastructure.MoneyManagement;
using Infrastructure.ScoreSystem;
using Infrastructure.Sound;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.UI.UIElements
{
    public class GameLoopHud : MonoBehaviour
    {
        [SerializeField] private GoToMenuButton _menuButton;
        [SerializeField] private ScoreIndicator _scoreInterface;
        [SerializeField] private HPBar _enemyHPBar;
        [SerializeField] private HPBar _playerHPBar;
        [SerializeField] private MoneyIndicator _moneyIndicator;
        [SerializeField] private SoundButton _soundButton;

        public void Construct(IMoney money,IScore score, IUIMediator uiMediator,ISound sound)
        {
            _soundButton.Construct(sound);
            _scoreInterface.Construct(score);
            _menuButton.Construct(uiMediator);
            _moneyIndicator.Construct(money);
        }

        public void ActivateHpBar(IHealth health,bool isEnemy)
        {
            if (isEnemy)
            {
                _enemyHPBar.Activate(health);   
            }
            else
            {
                _playerHPBar.Activate(health);
            }
        }
    }
}