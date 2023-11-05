using EntityComponents.EntityFacades;
using EntityComponents.Health;

namespace Infrastructure.UI.UIInfrostructure
{
    public interface IUIService
    {
        void Initialize();
        void EnableMainManu();

        void EnableShop();

        void EnableEndGameInterface();

        void DisableUI();
        void EnableGameLoopUI();
        void ShowCriticalDamageEffect();
        void ShowDamageEffect();
        void ActivateHPBar(IHealth health,bool isEnemy);
        void EnablePurchase();
    }
}