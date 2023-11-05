using System.Collections.Generic;
using Assets.SimpleSlider.Scripts;
using Infrastructure.Adds;
using Infrastructure.Factories;
using Infrastructure.GunShop;
using Infrastructure.MoneyManagement;
using Infrastructure.ResourceManagement;
using Infrastructure.Sound;
using Infrastructure.UI.UIInfrostructure;
using TMPro;
using UnityEngine;
namespace Infrastructure.UI.UIElements
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private GunShopSlider _shopLayout;
        [SerializeField] private GoToMenuButton _menuButton;
        [SerializeField] private BuyGunButton _buyGunButton;
        [SerializeField] private ApplyGunButton _applyGunButton;
        [SerializeField] private GunPriceTag _priceTag;
        [SerializeField] private GunName _nameTag;
        [SerializeField] private MoneyIndicator _moneyIndicator;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private GoToPurchaseButton _purchaseButton;
        [SerializeField] private RewardedAddsButton _addsButton;
        private GunSellWindow _activeGunWindow;
        private List<GunSellWindow> _gunWindows;
        private IGunShop _shop;

        public void Construct(IMoney money, IGunAssetProvider gunAssetProvider, IUIMediator uiMediator, IGunShop shop,
            IUIFactory uiFactory, ISound sound, IAddsService adds)
        {
            _addsButton.Construct(money, adds);
            _purchaseButton.Construct(uiMediator);
            _soundButton.Construct(sound); 
            _moneyIndicator.Construct(money);
            _shop = shop;
            _menuButton.Construct(uiMediator);
            _gunWindows = new List<GunSellWindow>();
            int i = 0;
            foreach (var gun in gunAssetProvider.GetAllGuns())
            {
                if (i != 0)
                {
                    _gunWindows.Add(uiFactory.CreateGunWindow(gun, this,_shopLayout.GetGridTransform(),i-1));   
                }
                i++;
            }
            _shopLayout.Initialize(_gunWindows,this);
            SelectGunWindow(0);
        }

        public void SelectGunWindow(int gunWindowIndex)
        {
            if (gunWindowIndex != -1)
            {
                _activeGunWindow = _gunWindows[gunWindowIndex];
            }
            if(_activeGunWindow == null) return;
            if (_shop.IsBought(_activeGunWindow.GunId))
            {
                _buyGunButton.Deactivate();
                _applyGunButton.Activate(_shop.CanActivate(_activeGunWindow.GunId));
                _priceTag.Disable();
            }
            else
            {
                _buyGunButton.Activate(_shop.CanBuy(_activeGunWindow.GunId));
                _applyGunButton.Deactivate();
                _priceTag.Init(_activeGunWindow.GunData.Price.ToString());
            }
            _nameTag.Init(_activeGunWindow.GunData.Name);
        }

        public void BuyGun()
        {
            _shop.BuyGun(_activeGunWindow.GunId);
            SelectGunWindow(-1);
            ActivateGun();
        }

        public void ActivateGun()
        {
            _shop.Select(_activeGunWindow.GunId);
            SelectGunWindow(-1);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}