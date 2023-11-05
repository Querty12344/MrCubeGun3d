using System.Collections.Generic;
using EntityComponents.ShootingSystem;
using Infrastructure.DataSavingSystem;
using Infrastructure.MoneyManagement;
using Infrastructure.ResourceManagement;

namespace Infrastructure.GunShop
{
    public class GunShop : IGunShop
    {
        private readonly List<GunTypeId> _boughtGuns;
        private readonly IGunAssetProvider _gunAssetProvider;
        private readonly IMoney _money;
        private readonly IPlayerConfig _playerConfig;
        private readonly ISaveLoadService _saveLoadService;

        public GunShop(IMoney money, ISaveLoadService saveLoadService, IPlayerConfig playerConfig,
            IGunAssetProvider gunAssetProvider)
        {
            _money = money;
            _saveLoadService = saveLoadService;
            _playerConfig = playerConfig;
            _gunAssetProvider = gunAssetProvider;
            _boughtGuns = saveLoadService.LoadBoughtGuns();

        }

        public void LoadData()
        {
            
        }
        public void Select(GunTypeId gunId)
        {
            _playerConfig.ActiveGunId = gunId;
        }

        public bool IsBought(GunTypeId gunId)
        {
            return _boughtGuns.Contains(gunId);
        }

        public bool CanActivate(GunTypeId gunId)
        {
            return gunId != _playerConfig.ActiveGunId && _boughtGuns.Contains(gunId);
        }

        public bool CanBuy(GunTypeId gunId)
        {
            var gunPrice = _gunAssetProvider.GetGun(gunId).Price;
            return _money.GetAmount() >= gunPrice && !_boughtGuns.Contains(gunId);
        }

        public void BuyGun(GunTypeId gunId)
        {
            if (_money.TryGetMoney(_gunAssetProvider.GetGun(gunId).Price))
            {
                _boughtGuns.Add(gunId);
                SaveData();
            }
        }

        private void SaveData()
        {
            _saveLoadService.SaveGunShopData(_boughtGuns);
        }
    }
}