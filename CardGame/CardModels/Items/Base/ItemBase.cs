using CardGame.GameObjectsUI;
using CardGame.ServiceObjects;
using System.Diagnostics;

namespace CardGame.CardModels.Items
{
    public class ItemBase : PropertyChangeObject, ICardModel
    {
        protected readonly int _iD;
        /// <summary>
        /// ID of item.
        /// </summary>
        public int ID
        {
            get => _iD;
        }

        protected string _name;
        /// <summary>
        /// Name of item.
        /// </summary>
        public string Name
        {
            get => _name;
        }

        protected readonly string _describe;
        /// <summary>
        /// Item describe.
        /// </summary>
        public string Describe
        {
            get => _describe;
        }

        protected readonly string _shortDescribe;
        /// <summary>
        /// Item short describe.
        /// </summary>
        public string ShortDescribe
        {
            get => _shortDescribe;
        }

        protected readonly string _exampleImageSource;
        /// <summary>
        /// Example image source of item.
        /// </summary>
        public string ExampleImageSource
        {
            get => _exampleImageSource;
        }


        private Brush _backgroundColor;
        /// <summary>
        /// Background color of item.
        /// </summary>
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        private Brush _strokeColor;
        /// <summary>
        /// Stroke color of item.
        /// </summary>
        public Brush StrokeColor
        {
            get => _strokeColor;
            set
            {
                _strokeColor = value;
                OnPropertyChanged(nameof(StrokeColor));
            }
        }

        private Brush _auraBrush;
        /// <summary>
        /// Aura brush of item.
        /// </summary>
        public Brush AuraBrush
        {
            get => _auraBrush;
            set
            {
                _auraBrush = value;
                OnPropertyChanged(nameof(AuraBrush));
            }
        }

        public ItemCard CardOvner;

        private static void DestroyCard(ItemCard card)
        {
            card.Destroy();
        }

        ~ItemBase()
        {
#if DEBUG
            Debug.WriteLine("Item has been destroyed.");
            // todo destroy it doesn't works
#endif
        }

    }
}
