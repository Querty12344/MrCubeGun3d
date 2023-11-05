using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class GoToMenuButton : MonoBehaviour
    {
        private IUIMediator _uiMediator;

        public void Construct(IUIMediator uiMediator)
        {
            _uiMediator = uiMediator;
        }

        public void GoToMenu()
        {
            _uiMediator.GoToMenu();
        }
    }
}