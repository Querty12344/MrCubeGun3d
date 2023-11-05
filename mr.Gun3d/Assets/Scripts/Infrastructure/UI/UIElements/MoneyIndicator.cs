using Infrastructure.MoneyManagement;
using TMPro;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class MoneyIndicator:MonoBehaviour
    {
        
        [SerializeField] private TMP_Text _text;
        private IMoney _money;
        
        public void Construct(IMoney money)
        {
            _money = money;
            _text.text = money.GetAmount().ToString();
            _money.OnMoneyChanged += UpdateAmount;
        }

        private void UpdateAmount(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}