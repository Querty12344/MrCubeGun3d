using EntityComponents.ShootingSystem;

namespace Infrastructure.DataSavingSystem
{
    public class PlayerConfig : IPlayerConfig
    {
        public GunTypeId ActiveGunId { get; set; }
    }
}