using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UserInteraction.Switchers
{
    [RequireComponent(typeof(Button))]
    public class SwitcherButton : MonoBehaviour
    {

        [SerializeField]
        
        private GameObject _appearingObj;


        [SerializeField] 
        
        private GameObject _disappearingObj;


        private Button _button;


        private void OnValidate()
        {
            
            _button = GetComponent<Button>();
        }


        private void Start()
        {

            _button.onClick.AddListener(() => 
            {

                _disappearingObj.SetActive(false);

                _appearingObj.SetActive(true);
            });
        }
    }
}