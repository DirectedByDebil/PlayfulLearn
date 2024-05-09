using UnityEngine;
using Initializations;
using UserInteraction.Switchers;

namespace LearningPrograms
{
    public class LearningProgramEditor : MonoBehaviour, IInitialization, ISwitchable
    {
        public event ISwitchable.SwitchHandler Switched;

        public void Initialize()
        {
            Switched += gameObject.SetActive;
        }

        public void OnSwitched(bool value)
        {
            Switched?.Invoke(value);
        }

        public void SaveLearningProgram()
        {

        }
    }
}