using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UserInteraction.Switchers
{
    [RequireComponent(typeof(Button))]
    public class Turner : MonoBehaviour
    {

        [SerializeField]
        
        private GameObject _obj;


        private Button _button;


        private void OnValidate()
        {
            
            _button = GetComponent<Button>();
        }


        private void Start()
        {

            _button.onClick.AddListener(() => { _obj.SetActive(!_obj.activeInHierarchy); });
        }
    }
}