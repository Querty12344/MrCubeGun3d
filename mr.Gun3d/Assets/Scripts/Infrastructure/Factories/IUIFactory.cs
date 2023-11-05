using Infrastructure.ResourceManagement.StaticData;
using Infrastructure.UI.UIElements;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        void Initialize();
        MainMenu CreateMainMenuHud();
        void CreateLoadingCurtain();
        PlayerLoseWindow CreateLoseWindow();
        ShopWindow CreateShop();
        GunSellWindow CreateGunWindow(GunStaticData gun, ShopWindow shopWindow,Transform[] grid,int i);
        GameLoopHud CreateGameLoop();
        void CreateCriticalDamageEffect();
        void CreateDamageEffect();
        PurchaseWindow CreatePurchase();
    }
}