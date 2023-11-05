using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI.UIElements
{
    public class BuyGunButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate(bool canBuy)
        {
            gameObject.SetActive(true);
            _button.interactable = canBuy;
        }
    }
}