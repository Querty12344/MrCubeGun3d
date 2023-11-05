using UnityEngine;

namespace Infrastructure.ResourceManagement
{
    public interface IUIAssetProvider
    {
        GameObject GetMainMenuHud();

        GameObject GetLoadingCurtain();
        GameObject GetLoseWindow();
        GameObject GetShopWindow();
        GameObject GetGunWindow();
        GameObject GetGameWindow();
        GameObject GetCriticalDamageEffect();
        GameObject GetDamageEffect();
        GameObject GetPurchase();
    }
}