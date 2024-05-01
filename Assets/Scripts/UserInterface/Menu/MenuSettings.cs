using UnityEngine;

namespace UserInterface.Menu
{
    public struct MenuSettings
    {
        public int ObjectsInRow
        {
            get { return _objectsInRow; }
            set
            {
                if (value > 0)
                    _objectsInRow = value;
            }
        }
        public Vector2 Padding { get; private set; }

        public Vector2 _startPosition;

        private int _objectsInRow;

        public MenuSettings(int objectsInRow, Vector2 padding, Vector2 startPosition)
        {
            _objectsInRow = 1;
            Padding = padding;
            _startPosition = startPosition;

            ObjectsInRow = objectsInRow;
        }
    }
}