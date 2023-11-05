using EntityComponents.EntityFacades;
using EntityComponents.Health;
using Infrastructure.Factories;
using Infrastructure.UI.UIElements;

namespace Infrastructure.UI.UIInfrostructure
{
    public class UIService : IUIService
    {
        private readonly IUIFactory _uiFactory;
        private GameLoopHud _gameLoopHud;
        private PlayerLoseWindow _loseWindow;
        private MainMenu _mainMenu;
        private ShopWindow _shop;
        private PurchaseWindow _purchase;

        public UIService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _uiFactory.Initialize();
        }

        public void EnableMainManu()
        {
            DisableUI();
            _mainMenu = _uiFactory.CreateMainMenuHud();
        }

        public void EnableShop()
        {
            DisableUI();
            _shop = _uiFactory.CreateShop();
        }

        public void EnableEndGameInterface()
        {
            DisableUI();
            _loseWindow = _uiFactory.CreateLoseWindow();
        }

        public void DisableUI()
        {
            if (_mainMenu != null)
                _mainMenu.Remove();
            if (_loseWindow != null)
                _loseWindow.Remove();
            if (_shop != null)
                _shop.Remove();
            if (_purchase != null)
                _purchase.Remove();
        }

        public void EnableGameLoopUI()
        {
            DisableUI();
            _gameLoopHud = _uiFactory.CreateGameLoop();
        }

        public void ShowCriticalDamageEffect()
        {
            _uiFactory.CreateCriticalDamageEffect();
        }

        public void ShowDamageEffect()
        {
            _uiFactory.CreateDamageEffect();
        }

        public void ActivateHPBar(IHealth health,bool isEnemy)
        {
            _gameLoopHud.ActivateHpBar(health,isEnemy);
        }

        public void EnablePurchase()
        {
            DisableUI();
            _purchase = _uiFactory.CreatePurchase();
        }
    }
}