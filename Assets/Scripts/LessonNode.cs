using UnityEngine;

[System.Serializable]
public struct LessonNode
{
    public Languages Language { get { return _language; } }

    [SerializeField] private Languages _language;

    public LessonTextContent lessonTextContent;
}