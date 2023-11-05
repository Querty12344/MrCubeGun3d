using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class GoToShopButton : MonoBehaviour
    {
        private IUIMediator _uiMediator;

        public void Construct(IUIMediator uiMediator)
        {
            _uiMediator = uiMediator;
        }

        public void GoToShop()
        {
            _uiMediator.GoToShop();
        }
    }
}