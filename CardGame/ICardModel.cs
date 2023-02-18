namespace CardGame
{
    public interface ICardModel
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ID.
        /// </summary>
        public int ID { get; }

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
