using UnityEngine;

[CreateAssetMenu(fileName = "NewTheoryPart", menuName = "TheoryPart", order = 52)]
public class TheoryPart : ScriptableObject
{
    [TextArea(5, 20)]
    public string Introduction, Usage;

    [Space] public Sprite Sprite;
}