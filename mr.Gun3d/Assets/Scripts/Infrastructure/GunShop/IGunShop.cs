using EntityComponents.ShootingSystem;

namespace Infrastructure.GunShop
{
    public interface IGunShop
    {
        void Select(GunTypeId gunId);
        bool IsBought(GunTypeId gunId);
        bool CanActivate(GunTypeId gunId);
        bool CanBuy(GunTypeId gunId);
        void BuyGun(GunTypeId gunId);
    }
}