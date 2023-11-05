using Infrastructure.Adds;
using Infrastructure.MoneyManagement;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class PurchaseWindow:MonoBehaviour
    {
        [SerializeField] private MoneyIndicator _moneyIndicator;
        [SerializeField] private RewardedAddsButton _addsButton;
        [SerializeField] private GoToShopButton _shopButton;

        public void Construct(IMoney money, IUIMediator uiMediator, IAddsService addsService)
        {
            _addsButton.Construct(money, addsService);
            _moneyIndicator.Construct(money);
            _shopButton.Construct(uiMediator);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}