using Infrastructure.MoneyManagement;
using Infrastructure.Sound;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private StartGameButton _startGameButton;
        [SerializeField] private GoToShopButton _shopButton;
        [SerializeField] private MoneyIndicator _moneyIndicator;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private GoToPurchaseButton _purchaseButton;

        public void Construct(IMoney money,IUIMediator uiMediator,ISound sound)
        {
            _purchaseButton.Construct(uiMediator);
            _soundButton.Construct(sound);
            _shopButton.Construct(uiMediator);
            _startGameButton.Construct(uiMediator);
            _moneyIndicator.Construct(money);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}