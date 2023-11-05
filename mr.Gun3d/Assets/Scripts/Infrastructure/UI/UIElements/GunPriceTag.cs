using System;
using TMPro;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    
    public class GunPriceTag:MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        public void Init(string price)
        {
            gameObject.SetActive(true);
            _text.text = price;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}