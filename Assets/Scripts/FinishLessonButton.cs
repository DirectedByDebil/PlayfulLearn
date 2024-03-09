using UnityEngine;
using Button = UnityEngine.UI.Button;

[RequireComponent(typeof(Button))]
public class FinishLessonButton : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(delegate
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        });
    }
}