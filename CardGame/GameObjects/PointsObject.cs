using CardGame.ServiceObjects;

namespace CardGame.GameObjects
{
    internal class PointsObject : PropertyChangeObject
    {
        // Problem with "_points" privacy and methods like "Heal(int)"
        protected int _points;
        /// <summary>
        /// Points property.
        /// </summary>
        public int Points
        {
            get => _points;
            private set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
                PointsValueChanged?.Invoke(_points);
            }
        }

        public Action<int> PointsValueChanged;
    }
}
