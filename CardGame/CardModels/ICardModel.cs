using CardGame.GameObjectsUI;

namespace CardGame
{
    internal interface ICardModel
    {
        /// <summary>
        /// ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Item describe.
        /// </summary>
        public string Describe { get; }

        /// <summary>
        /// Item short describe.
        /// </summary>
        public string ShortDescribe { get; }

        /// <summary>
        /// Example image source.
        /// </summary>
        public string ExampleImageSource { get; }

        /// <summary>
        /// Background color.
        /// </summary>
        public Brush BackgroundColor { get; set; }

        /// <summary>
        /// Stroke color.
        /// </summary>
        public Brush StrokeColor { get; set; }

        /// <summary>
        /// Aura brush.
        /// </summary>
        public Brush AuraBrush { get; set; }
    }
}
