using EntityComponents.ShootingSystem;

namespace Infrastructure.DataSavingSystem
{
    public interface IPlayerConfig
    {
        GunTypeId ActiveGunId { get; set; }
    }
}