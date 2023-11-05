using Infrastructure.Factories;
using Infrastructure.GameCore;
using Infrastructure.GameCore.GameStates;

namespace Infrastructure.UI.UIInfrostructure
{
    public class UIMediator : IUIMediator
    {
        private readonly IGame _game;
        private readonly IGameStatesHandler _gameStates;
        private readonly IUIService _uiService;
        

        public UIMediator(IGameStatesHandler gameStates, IUIService uiService, IGame game)
        {
            _gameStates = gameStates;
            _uiService = uiService;
            _game = game;
        }

        public void StartGame()
        {
            var mainMenuState = (MainMenuState)_gameStates.GetState<MainMenuState>();
            mainMenuState.StartGame();
        }

        public void RestartGame()
        {
            _uiService.DisableUI();
            _game.Restart();
        }

        public void GoToShop()
        {
            _uiService.EnableShop();
        }
        

        public void GoToPurchase()
        {
            _uiService.EnablePurchase();
        }

        public void GoToMenu()
        {
            if (_game.IsRuning)
                _game.Exit();
            else
                _uiService.EnableMainManu();
        }
    }
}