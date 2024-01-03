using UnityEngine;

[CreateAssetMenu(fileName = "NewPracticalPart", menuName = "PracticalPart", order = 53)]
public class PracticalPart : ScriptableObject
{
    [TextArea(5, 20)]
    public string Description;

    [Space] public Sprite Sprite;
    public UnityEditor.SceneAsset Scene;
}