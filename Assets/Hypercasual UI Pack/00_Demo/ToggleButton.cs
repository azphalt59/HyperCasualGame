using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualUIPackDemo
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField]
        Sprite _toggleOn;
        [SerializeField]
        Sprite _toggleOff;

        [SerializeField]
        bool _startToggled;

        Image _button;
        bool _toggled;

        void Awake()
        {
            _button = GetComponent<Image>();
        }

        private void OnValidate()
        {
            if (!_toggleOn || !_toggleOff) return;
            if (!_button)
                _button = GetComponent<Image>();
            _button.sprite = _startToggled ? _toggleOn : _toggleOff;
        }

        private void Start()
        {
            _toggled = _startToggled;
            _button.sprite = _startToggled ? _toggleOn : _toggleOff;
        }

        public void Toggle()
        {
            _toggled = !_toggled;
            _button.sprite = _toggled ? _toggleOn : _toggleOff;
        }
    }
}
