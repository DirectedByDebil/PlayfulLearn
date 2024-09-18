using System;

namespace UserInterface
{
    public sealed class ToggleModel<T>
    {

        public event Action<T, bool> Changed;


        private readonly T _object;


        public ToggleModel(T obj)
        {

            _object = obj;
        }


        public void InvokeChanged(bool isOn)
        {

            Changed?.Invoke(_object, isOn);
        }
    }
}
