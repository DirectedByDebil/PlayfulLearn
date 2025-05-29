using UnityEngine;
using UnityEditor;

namespace Lessons
{

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LessonData))]
    public sealed class LessonDataDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;


            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);


            SerializedProperty nameOfLesson = property.FindPropertyRelative("NameOfLesson");

            SerializedProperty icon = property.FindPropertyRelative("Icon");

            SerializedProperty difficulty = property.FindPropertyRelative("Difficulty");


            if(property.isExpanded)
            {

                EditorGUILayout.PropertyField(nameOfLesson);

                EditorGUILayout.PropertyField (icon);

                EditorGUILayout.PropertyField (difficulty);
            }



            EditorGUI.EndProperty();
        }


    }
#endif
}
