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
        /// CardModel describe.
        /// </summary>
        public string Describe
        {
            get => _describe;
        }

        protected readonly string _shortDescribe;
        /// <summary>
        /// CardModel short describe.
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

        private int _numberOfUses;
        /// <summary>
        /// Number of remaining uses.
        /// </summary>
        public int NumberOfUses
        {
            get => _numberOfUses;
            set
            {
                _numberOfUses = value;
                OnPropertyChanged(nameof(NumberOfUses));
            }
        }

        public virtual void ItemFunction(ICardModel[] enemies, ICardModel[] allies, ICardModel[] selectedEnemies, ICardModel[] selectedAllies) { }

        public ItemCard CardOvner;
        public ItemBase(string name, int iD, string describe, string shortDescribe, string exampleImageSource, int numberOfUses, ItemTypeEnum itemType, uint maxCardToSelect, Brush backgroundColor = null, Brush strokeColor = null)
        {
            _name = name;
            _iD = iD;
            _describe = describe;
            _shortDescribe = shortDescribe;
            _exampleImageSource = exampleImageSource;
            _numberOfUses = numberOfUses;
            _itemType = itemType;

            //error catcher
            if (_itemType == ItemTypeEnum.ToOneEnemy ||
                _itemType == ItemTypeEnum.ToOneAllie)
                _maxCardToSelect = 1;
            else if (_itemType == ItemTypeEnum.ToAll)
                _maxCardToSelect = 0;
            else
                _maxCardToSelect = maxCardToSelect;

            _backgroundColor = backgroundColor == null ? Brush.Orange : backgroundColor;
            _strokeColor = strokeColor == null ? Brush.White : strokeColor;
        }

        private static void DestroyCard(CardBase card)
        {
            card.Destroy();
        }

        ~ItemBase()
        {
#if DEBUG
            Debug.WriteLine("CardModel has been destroyed.");
            // todo destroy it doesn't works
#endif
        }

        public enum ItemTypeEnum
        {
            ToOneEnemy,
            ToMoreThanOneEnemy,
            ToAllEnemies,

            ToOneAllie,
            ToMoreThanOneAllie,
            ToAllAllies,

            ToAll
        }

        private readonly ItemTypeEnum _itemType;
        /// <summary>
        /// Type of item by used.
        /// </summary>
        public ItemTypeEnum ItemType
        {
            get => _itemType;
        }

        private readonly uint _maxCardToSelect = 1;
        /// <summary>
        /// Number of card to select with using the item.
        /// </summary>
        public uint MaxCardToSelect
        {
            get => _maxCardToSelect;
        }

    }
}
