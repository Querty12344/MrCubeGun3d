using System.Collections.Generic;
using EntityComponents.ShootingSystem;

namespace Infrastructure.DataSavingSystem
{
    public interface ISaveLoadService
    {
        void SaveMoneyAmount(int amount);
        void SaveGunShopData(List<GunTypeId> boughtGuns);
        int LoadMoneyAmount();
        List<GunTypeId> LoadBoughtGuns();
        void SaveScore(int score);
    }
}