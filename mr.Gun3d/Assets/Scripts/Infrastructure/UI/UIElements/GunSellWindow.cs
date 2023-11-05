using EntityComponents.ShootingSystem;
using Infrastructure.ResourceManagement.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI.UIElements
{
    public class GunSellWindow : MonoBehaviour
    {
        public GunTypeId GunId;
        public GunStaticData GunData;

        public void Construct(GunStaticData gunData)
        {
            GunId = gunData.GunType;
            GunData = gunData;
            Instantiate(gunData.Prefab, transform);
        }
    }
}