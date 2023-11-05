using System.Collections.Generic;
using System.Linq;
using EntityComponents.ShootingSystem;
using Infrastructure.Factories;
using Infrastructure.ResourceManagement.StaticData;

namespace Infrastructure.ResourceManagement
{
    public class GunAssetProvider : IGunAssetProvider

    {
        private readonly StaticData.StaticData _staticData;
        private Dictionary<GunTypeId, GunStaticData> _guns;
        private List<GunTypeId> _gunTypes;

        public GunAssetProvider(StaticData.StaticData staticData)
        {
            _staticData = staticData;

            _guns = new Dictionary<GunTypeId, GunStaticData>();
        }

        public void InitializeGuns()
        {
            _gunTypes = new List<GunTypeId>();
            var gunsData = _staticData.GetAllGuns();
            _guns = new Dictionary<GunTypeId, GunStaticData>();
            foreach (var gun in gunsData)
            {
                if(!_gunTypes.Contains(gun.GunType))
                    _gunTypes.Add(gun.GunType);
            }
            foreach (var gun in gunsData) _guns.Add(gun.GunType, gun);
        }
        
        public GunStaticData[] GetAllGuns()
        {
            return _guns.Values.ToArray();
        }
        public GunStaticData GetGun(GunTypeId gunType)
        {
            return _guns[gunType];
        }
    }
}