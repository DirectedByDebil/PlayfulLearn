using UnityEngine;

[System.Serializable]
public struct LessonNode
{
    public Languages Language { get { return _language; } }
    public LessonTextContent Texts { get { return _lessonTextContent; } }

    [SerializeField] private Languages _language;
    [SerializeField] private LessonTextContent _lessonTextContent;
}