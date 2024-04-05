using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UserInteraction
{
    [RequireComponent(typeof(Button))]
    public class SwitcherButton : MonoBehaviour
    {
        [SerializeField] private GameObject _appearingObj, _disappearingObj;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() => 
            {
                _disappearingObj.SetActive(false);
                _appearingObj.SetActive(true);
            });
        }
    }
}