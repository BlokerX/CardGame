using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Items
{
    public class ItemBase : ICardModel
    {
        public string Name => throw new NotImplementedException();

        public int ID => throw new NotImplementedException();

        public string ExampleImageSource => throw new NotImplementedException();

        public Brush BackgroundColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public Brush StrokeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public Brush AuraBrush { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
