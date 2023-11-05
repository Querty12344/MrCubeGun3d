using Infrastructure.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UI.UIElements
{
    public class SoundButton:MonoBehaviour
    {
        private ISound _sound;
        [SerializeField] private Color _ableColor;
        [SerializeField] private Color _disabledColor;
        private Image _image;

        public void Construct(ISound sound)
        {
            _sound = sound;
            _image = GetComponent<Image>();
            _image.color = _sound.IsOn ? _ableColor : _disabledColor;
        }

        public void Click()
        {
            if (_sound.IsOn)
            {
                _image.color = _disabledColor;
                _sound.OffSound();
            }
            else
            {
                _sound.OnSound();
                _image.color = _ableColor;
            }
        }
    }
}