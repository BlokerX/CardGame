using System.ComponentModel;

namespace CardGame.ServiceObjects
{
    /// <summary>
    /// Object supports property changed.
    /// </summary>
    public class PropertyChangeObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
