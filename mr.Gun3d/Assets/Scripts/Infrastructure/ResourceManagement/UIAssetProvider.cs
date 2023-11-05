using Infrastructure.Constants;
using UnityEngine;

namespace Infrastructure.ResourceManagement
{
    public class UIAssetProvider : IUIAssetProvider

    {
        public GameObject GetMainMenuHud()
        {
            return Resources.Load<GameObject>(ResourcesPath.MainMenuUIPath);
        }

        public GameObject GetLoadingCurtain()
        {
            return Resources.Load<GameObject>(ResourcesPath.LoadingCurtainPath);
        }

        public GameObject GetLoseWindow()
        {
            return Resources.Load<GameObject>(ResourcesPath.LoseWindow);
        }

        public GameObject GetShopWindow()
        {
            return Resources.Load<GameObject>(ResourcesPath.ShopWindow);
        }

        public GameObject GetGunWindow()
        {
            return Resources.Load<GameObject>(ResourcesPath.GunWindow);
        }

        public GameObject GetGameWindow()
        {
            return Resources.Load<GameObject>(ResourcesPath.GameUI);
        }

        public GameObject GetCriticalDamageEffect()
        {
            return Resources.Load<GameObject>(ResourcesPath.CriticalDamageEffect);
        }

        public GameObject GetDamageEffect()
        {
            return Resources.Load<GameObject>(ResourcesPath.DamageEffect);
        }

        public GameObject GetPurchase()
        {
            return Resources.Load<GameObject>(ResourcesPath.Purchase);
        }
    }
}