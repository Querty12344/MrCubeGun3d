using Infrastructure.Adds;
using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class LoadingCurtain : ILoadingCurtain
    {
        private readonly IUIFactory _uiFactory;
        private readonly IAddsService _addsService;

        public LoadingCurtain(IUIFactory uiFactory,IAddsService addsService)
        {
            _uiFactory = uiFactory;
            _addsService = addsService;
        }

        public void Show()
        {
            _uiFactory.CreateLoadingCurtain();
            _addsService.ShowFullScreenAdd(_uiFactory.CreateLoadingCurtain);
        }
        
    }
}