using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lesson", menuName = "NewLesson", order = 60)]
public class NewLesson : ScriptableObject, IInitialization
{
    public Dictionary<Languages, LessonTextContent> Test = new();
    
    public string NameOfLesson;
    public Categories Category;
    
    [Space] public Sprite BackgroundSprite;
    [Space] public UnityEditor.SceneAsset PracticeScene;

    [SerializeField] private List<LessonNode> LessonText = new ();

    public void Initialize()
    {
        foreach(var node in LessonText)
        {
            Test.TryAdd(node.Language, node.lessonTextContent);
        }
    }
}