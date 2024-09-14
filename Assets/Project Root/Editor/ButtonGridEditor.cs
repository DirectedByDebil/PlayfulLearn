using UnityEditor;
using UnityEngine;

namespace UserInterface
{

    [CustomEditor(typeof(ButtonGrid), true)]

    [CanEditMultipleObjects]
    public sealed class ButtonGridEditor : UIObjectEditor
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


        protected override void OnEnable()
        {

            base.OnEnable();

            _grid = (ButtonGrid)target;
        }
    }
}
