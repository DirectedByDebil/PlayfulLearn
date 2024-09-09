using UnityEditor;
using UnityEngine;

namespace UserInterface
{

    [CustomEditor(typeof(ButtonGrid))]

    [CanEditMultipleObjects]
    public sealed class ButtonGridEditor : Editor
    {

        private ButtonGrid _grid;

        private int _buttonsCount;


        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();


            EditorGUILayout.Space();


            if (GUILayout.Button("Update Buttons"))
            {

                _grid.UpdateButtons();

                _buttonsCount = _grid.transform.childCount;
            }


            if(_buttonsCount > 0)
            {

                EditorGUILayout.HelpBox("Buttons: " + _buttonsCount
                    
                    , MessageType.Info);
            }
        }


        private void OnEnable()
        {

            _grid = (ButtonGrid)target;
        }
    }
}
