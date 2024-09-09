using System;

namespace Core
{
    public sealed class SelectableModel<T>
    {

        public event Action<T> Selected;


        private T _selectable;


        public void SetSelectable(T selectable)
        {

            _selectable = selectable;
        }


        public void SelectObject()
        {

            Selected?.Invoke(_selectable);
        }
    }
}
