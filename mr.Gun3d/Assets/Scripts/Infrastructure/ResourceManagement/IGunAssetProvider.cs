using EntityComponents.ShootingSystem;
using Infrastructure.Factories;
using Infrastructure.ResourceManagement.StaticData;

namespace Infrastructure.ResourceManagement
{
    public interface IGunAssetProvider
    {
        GunStaticData GetGun(GunTypeId gunType);
        void InitializeGuns();
        GunStaticData[] GetAllGuns();
    }
}