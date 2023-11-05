using Infrastructure.Adds;
using Infrastructure.GunShop;
using Infrastructure.MoneyManagement;
using Infrastructure.ResourceManagement;
using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.ScoreSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.Sound;
using Infrastructure.UI.UIElements;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IGunAssetProvider _gunAssetProvider;
        private readonly IGunShop _gunShop;
        private readonly IScore _score;
        private readonly GameSettingsProvider _settingsProvider;
        private readonly IMoney _money;
        private readonly IAddsService _addsService;
        private readonly IUIAssetProvider _uiAssetProvider;
        private IUIMediator _uiMediator;
        private ISound _sound;

        public UIFactory(IUIAssetProvider uiAssetProvider,
            DiContainer container,
            IGunAssetProvider gunAssetProvider,
            IGunShop gunShop,
            IScore score,
            GameSettingsProvider settingsProvider,
            IMoney money,
            IAddsService addsService, ISound sound)
        {
            _uiAssetProvider = uiAssetProvider;
            _container = container;
            _gunAssetProvider = gunAssetProvider;
            _gunShop = gunShop;
            _score = score;
            _settingsProvider = settingsProvider;
            _money = money;
            _addsService = addsService;
            _sound = sound;
        }

        public void Initialize()
        {
            _uiMediator = _container.Instantiate<UIMediator>();
        }

        public MainMenu CreateMainMenuHud()
        {
            var mainMenuHud = _uiAssetProvider.GetMainMenuHud();
            var mainMenu = Object.Instantiate(mainMenuHud).GetComponent<MainMenu>();
            mainMenu.Construct(_money,_uiMediator,_sound);
            return mainMenu;
        }

        public void CreateLoadingCurtain()
        {
            GameObject.Instantiate(_uiAssetProvider.GetLoadingCurtain());
        }

        public PlayerLoseWindow CreateLoseWindow()
        {
            var loseWindowPrefab = _uiAssetProvider.GetLoseWindow();
            var loseWindow = Object.Instantiate(loseWindowPrefab).GetComponent<PlayerLoseWindow>();
            loseWindow.Construct(_uiMediator,_score);
            return loseWindow;
        }

        public ShopWindow CreateShop()
        {
            var shopWindowPrefab = _uiAssetProvider.GetShopWindow();
            var shop = Object.Instantiate(shopWindowPrefab).GetComponent<ShopWindow>();
            shop.Construct(_money,_gunAssetProvider, _uiMediator, _gunShop, this,_sound, _addsService);
            return shop;
        }

        public GunSellWindow CreateGunWindow(GunStaticData gun, ShopWindow shopWindow,Transform[] grid,int i)
        {
            var gunWindowPrefab = _uiAssetProvider.GetGunWindow();
            var gunWindowObject = Object.Instantiate(gunWindowPrefab,grid[i]);
            var gunWindow = gunWindowObject.GetComponent<GunSellWindow>();
            gunWindow.Construct(gun);
            return gunWindow;
        }

        public GameLoopHud CreateGameLoop()
        {
            var gameLoopWindowPrefab = _uiAssetProvider.GetGameWindow();
            var gameLoopWindowObject = Object.Instantiate(gameLoopWindowPrefab);
            var gameWindow = gameLoopWindowObject.GetComponent<GameLoopHud>();
            gameWindow.Construct(_money,_score, _uiMediator,_sound);
            return gameWindow;
        }

        public void CreateCriticalDamageEffect()
        {
            var criticalDamagePrefab = _uiAssetProvider.GetCriticalDamageEffect();
            var criticalDamageObject = Object.Instantiate(criticalDamagePrefab);
            var criticalDamageEffect = criticalDamageObject.GetComponent<UIEffect>();
            criticalDamageEffect.Construct(_settingsProvider.OptimizationSettings.UIEffectsTime);
        }

        public void CreateDamageEffect()
        {
            var damagePrefab = _uiAssetProvider.GetDamageEffect();
            var damageObject = Object.Instantiate(damagePrefab);
            var damageEffect = damageObject.GetComponent<UIEffect>();
            damageEffect.Construct(_settingsProvider.OptimizationSettings.UIEffectsTime);
        }

        public PurchaseWindow CreatePurchase()
        {
            var purchasePrefab = _uiAssetProvider.GetPurchase();
            var purchaseObject = Object.Instantiate(purchasePrefab);
            var purchase = purchaseObject.GetComponent<PurchaseWindow>();
            purchase.Construct(_money, _uiMediator, _addsService);
            return purchase;
        }
    }
}