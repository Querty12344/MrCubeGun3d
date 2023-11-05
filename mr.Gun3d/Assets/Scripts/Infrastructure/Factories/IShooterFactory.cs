namespace EntityComponents.ShootingSystem
{
    public interface IShooterFactory
    {
        OnStairsShooter CreateOnStairsShooter(IGunHolder gunHolder);
        AutoAimShooter CreateAutoAimShooter(IGunHolder gunHolder);
        InRunShooter CreateInRunShooter(IGunHolder gunHolder);
    }
}