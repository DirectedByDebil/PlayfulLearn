using UnityEditor;
using UnityEngine;

namespace LessonsPractices
{

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(LessonPractice))]
    public sealed class LessonPracticeDrawer : PropertyDrawer
    {


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {


            EditorGUI.BeginProperty(position, label, property);


            SerializedProperty nameOfLesson = property.FindPropertyRelative("NameOfLesson");

            SerializedProperty practiceType = property.FindPropertyRelative("PracticeType");

            SerializedProperty asset = property.FindPropertyRelative("Asset");


            SerializedProperty questions = property.FindPropertyRelative("Questions");


            SerializedProperty sceneName = property.FindPropertyRelative("SceneName");

            SerializedProperty codeLines = property.FindPropertyRelative("CodeLines");
            


            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if(property.isExpanded)
            {

                DrawProperties(nameOfLesson, asset, practiceType);


                PracticeType type = (PracticeType)practiceType.enumValueIndex;

                switch (type)
                {

                    case PracticeType.Test:

                        DrawProperties(questions);
                        break;

                    case PracticeType.MiniGame:

                        DrawProperties(sceneName, codeLines);
                        break;
                }

            }


            EditorGUI.EndProperty();
        }


        private void DrawProperties(params SerializedProperty[] properties)
        {

            foreach (SerializedProperty property in properties)
            {

                EditorGUILayout.PropertyField(property);

                EditorGUILayout.Space();
            }
        }
    }
#endif
}
