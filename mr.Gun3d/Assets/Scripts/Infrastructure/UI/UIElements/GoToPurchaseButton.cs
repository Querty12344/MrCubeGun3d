using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class GoToPurchaseButton:MonoBehaviour
    {
        private IUIMediator _uiMediator;

        public void Construct(IUIMediator uiMediator)
        {
            _uiMediator = uiMediator;
        }

        public void GoToPurchase()
        {
            _uiMediator.GoToPurchase();
        }
    }
}