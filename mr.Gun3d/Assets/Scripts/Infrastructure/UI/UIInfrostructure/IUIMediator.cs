namespace Infrastructure.UI.UIInfrostructure
{
    public interface IUIMediator
    {
        
        void StartGame();
        void GoToMenu();
        void RestartGame();
        void GoToShop();
        void GoToPurchase();
    }
}