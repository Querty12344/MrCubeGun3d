using TMPro;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class GunName : MonoBehaviour
    {

        [SerializeField] private TMP_Text _text;

        public void Init(string name)
        {
            _text.text = name;
        }
        
    }
}