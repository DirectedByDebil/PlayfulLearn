using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class LessonsDB : EditorWindow
{
    private ReorderableList _list = null;
    private SerializedObject _table = null;
    private SerializedProperty _listOfLessons;

    private const int TOP_PADDING = 2;
    private const string HELP_TEXT = "We don't have table of lessons!";

    private Rect _helpRect = new Rect(0, 0, 600f, 200f);

    [MenuItem("Game/LessonsDB")]
    private static void OpenMenu()
    {
        GetWindow<LessonsDB>("View LessonsDB");
    }

    private void OnEnable()
    {
        TableOfLessons tableOfLessons = (TableOfLessons)AssetDatabase.LoadAssetAtPath("Assets/Resources/NewLessonsDB.asset", typeof(TableOfLessons));

        if (tableOfLessons)
        {
            _table = new SerializedObject(tableOfLessons);
            _listOfLessons = _table.FindProperty("Lessons");

            _list = new ReorderableList(_table, _listOfLessons, true, true, true, true);

            _list.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Lesson DB Lessons");
            _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);

                rect.y += TOP_PADDING;
                rect.height = EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, 200, rect.height), element.FindPropertyRelative("NameOfLesson"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(250, rect.y, 200, rect.height), element.FindPropertyRelative("Theory"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(475, rect.y, 200, rect.height), element.FindPropertyRelative("Practice"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(700, rect.y, 150, rect.height), element.FindPropertyRelative("Category"), GUIContent.none);
            };
        }
    }
    private void OnInspectorUpdate()
    {
        Repaint();
    }
    private void OnGUI()
    {
        if (_table != null)
        {
            _table.Update();
            _list.DoLayoutList();
            _table.ApplyModifiedProperties();
        }
        else
        {
            EditorGUI.HelpBox(_helpRect, HELP_TEXT, MessageType.Warning);
        }
    }
}