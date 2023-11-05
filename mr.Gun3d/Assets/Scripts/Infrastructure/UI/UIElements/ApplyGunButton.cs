using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI.UIElements
{
    public class ApplyGunButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate(bool canApply)
        {
            gameObject.SetActive(true);
            _button.interactable = canApply;
        }
    }
}