using UnityEngine;
using UnityEditor;

namespace LessonsPractices.Blocks
{

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(Block))]
    public sealed class BlockDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);


            position.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);


            if(property.isExpanded)
            {

                DrawBlock(property, position);
            }

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            int totalLine = 1;


            if (property.isExpanded)
            {

                totalLine = 6;
            }

            return EditorGUIUtility.singleLineHeight * totalLine +
                EditorGUIUtility.standardVerticalSpacing * (totalLine - 1);
        }



        private void DrawBlock(SerializedProperty property, Rect position)
        {

            SerializedProperty blockType = property.FindPropertyRelative("BlockType");

            SerializedProperty description = property.FindPropertyRelative("Description");

            
            float height = EditorGUIUtility.singleLineHeight * 1.2f;
            

            DrawProperty(blockType, ref position, height);


            BlockType type = (BlockType)blockType.enumValueIndex;


            switch (type)
            {

                case BlockType.Condition:

                    DrawProperty(property, "ConditionType", ref position, height);
                    break;


                case BlockType.Action:

                    DrawProperty(property, "ActionType", ref position, height);
                    break;


                case BlockType.Animation:

                    DrawProperty(property, "AnimationType", ref position, height);
                    break;
            }


            DrawProperty(property, "IsAnswer", ref position, height);


            position.y += height;

            description.stringValue = EditorGUI.TextArea(position, description.stringValue);
        }


        private void DrawProperty(SerializedProperty property, ref Rect position, float height)
        {

            position.y += height;

            EditorGUI.PropertyField(position, property);
        }


        private void DrawProperty(SerializedProperty parent, string name, ref Rect position, float height)
        {

            position.y += height;


            SerializedProperty property = parent.FindPropertyRelative(name);

            EditorGUI.PropertyField(position, property);
        }
    }
#endif
}