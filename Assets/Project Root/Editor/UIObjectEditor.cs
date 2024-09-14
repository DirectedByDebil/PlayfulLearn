using UnityEditor;

namespace UserInterface
{

    [CustomEditor(typeof(UIObject), true)]

    [CanEditMultipleObjects]
    public class UIObjectEditor : Editor
    {

        private SerializedProperty _hasManyStates;

        private SerializedProperty _state;

        private SerializedProperty _states;



        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();


            EditorGUI.BeginChangeCheck();

            serializedObject.Update();


            if(_hasManyStates.boolValue)
            {

                EditorGUILayout.PropertyField(_states);
            }
            else
            {

                EditorGUILayout.PropertyField(_state);
            }


            serializedObject.ApplyModifiedProperties();
            
            EditorGUI.EndChangeCheck();
        }


        protected virtual void OnEnable()
        {

            _hasManyStates = serializedObject.FindProperty("_hasManyStates");

            _state = serializedObject.FindProperty("_state");
            
            _states = serializedObject.FindProperty("_states");
        }
    }
}