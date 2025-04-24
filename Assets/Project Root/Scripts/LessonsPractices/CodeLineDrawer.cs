using UnityEngine;
using UnityEditor;

namespace LessonsPractices
{

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(CodeLine))]
    public sealed class CodeLineDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);


            position.height = EditorGUIUtility.singleLineHeight;
            
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);


            if(property.isExpanded)
            {

                SerializedProperty isReadOnly = property.FindPropertyRelative("IsReadOnly");

                SerializedProperty code = property.FindPropertyRelative("Code");

                SerializedProperty description = property.FindPropertyRelative("Description");


                DrawCode(position, isReadOnly, description, code);
            }


            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            int totalLine = 1;


            if(property.isExpanded)
            {

                SerializedProperty isReadOnly = property.FindPropertyRelative("IsReadOnly");

                if(isReadOnly.boolValue)
                {
                    totalLine = 6;
                }
                else
                {

                    totalLine = 3;
                }
            }

            return EditorGUIUtility.singleLineHeight * totalLine +
                EditorGUIUtility.standardVerticalSpacing * (totalLine - 1);
        }


        private void DrawCode(Rect position, SerializedProperty isReadOnly,
            SerializedProperty description, SerializedProperty code)
        {

            float height = EditorGUIUtility.singleLineHeight;

            position.y += height;

            EditorGUI.PropertyField(position, isReadOnly);


            if(isReadOnly.boolValue)
            {

                position.y += height;

                position.height = 4 * height;

                code.stringValue = EditorGUI.TextArea(position, code.stringValue);
            }
            else
            {

                position.y += height;

                EditorGUI.PropertyField(position, description);
            }
        }
    }

#endif
}