﻿using CardGame.Characters;
using CardGame.ViewModels;
using System.Diagnostics;

namespace CardGame.GameObjectsUI;

public partial class CharacterCard : CardBase
{
    private CharacterCard()
    {
        InitializeComponent();
    }

    ~CharacterCard()
    {
#if DEBUG
        // todo destroy it doesnt works
        Debug.WriteLine("Card has been destroyed successfull!");
#endif
    }

    public CharacterCard(MagicCharacter character) : this()
    {
        BindingContext = new MagicCharacterCardViewModel(character);
        (BindingContext as MagicCharacterCardViewModel).Character.CardOvner = this;
    }

    public CharacterCard(CharacterBase character) : this()
    {
        BindingContext = new CharacterCardViewModel(character);
        (BindingContext as CharacterCardViewModel).Character.CardOvner = this;
    }

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        if (this.Height / 2.5 != this.Width)
            this.SizeAllocated(this.Height / 2.5, this.Height);
        //ImgBorder.StrokeShape = new RoundRectangle() { CornerRadius = 10 };
    }


    private void TapGestureRecognizer_Tapped(object sender, EventArgs e) => OnCardTaped?.Invoke(this);

}