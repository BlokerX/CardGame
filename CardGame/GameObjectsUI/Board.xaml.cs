﻿using CardGame.Characters;
using CardGame.GameObjects;
using CardGame.ViewModels;

namespace CardGame.GameObjectsUI;

public partial class Board : ContentPage
{
    /// <summary>
    /// Card per person count.
    /// </summary>
    private const int cardPerPerson = 32;

    /// <summary>
    /// Highlight card animation time.
    /// </summary>
    private const int highlightCardAnimationTime = 500;

    /// <summary>
    /// Players in game.
    /// </summary>
    private readonly List<Player> players;

    public Board()
    {
        InitializeComponent();

        players = new List<Player>() { null, null };

        //todo rozdawanie kart graczom i ich tworzenie
        players[0] = new Player();
        players[1] = new ComputerPlayer();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Build(new()
            {    
                Cards = Player.GetCards(cardPerPerson)
            });
        }

        players[0].SetLobby(PlayerCards);
        players[0].SetBoard(PlayerBoard);
        players[1].SetBoard(ComputerBoard);

        AddClickEventToSelect_PlayerCards();

        this.Loaded += (s, e) => ComputerTurn();
        //todo change loaded to start event
    }

    #region Events methods
    // powiązania istnieją wyłącznie z tym kodem,
    // po przeniesieniu tego na kartę Player.cs będzie można zlikwidować
    // te metody
    // * Wyjątkiem są objekty tali gracza i przypisywanie usuwania z niej (trzeba zrobić) * //
    // ----------------------------- //

    #endregion

    // domyślna akcja kliknięcia karty gracza
    private void AddClickEventToSelect_PlayerCards()
    {
        foreach (CardBase card in PlayerCards.Children.Cast<CardBase>())
        {
            card.OnCardTaped += players[0].ThrowNewCard;
        }
    }

    #region Game logic

    #region Player turn

    // Wybieranie karty atakowanej:
    private void OnComputerCardClickedPlayerTargeted(CardBase card)
    {
        if (players[0].TurnFaze != Player.TurnFazeEnum.SelectingEnemyCard)
            return;

        players[0].TargetedCard = card;

        players[0].TurnFaze = Player.TurnFazeEnum.Attacking;

        players[0].HighlightTargetedCard((d, b) => AttackTargetCard());
    }

    private void AttackTargetCard()
    {
        CharacterBase myCharacter = (players[0].ChosenCard.BindingContext as CharacterCardViewModel).Character;
        CharacterBase enemyCharacter = (players[0].TargetedCard.BindingContext as CharacterCardViewModel).Character;

        // Normal attack
        if (players[0].AttackType == Player.AttackTypeEnum.Attack)
        {
            myCharacter.Attack(enemyCharacter);
            players[0].SpecialPoints++;
        }
        // Special attack
        else if (players[0].AttackType == Player.AttackTypeEnum.SpecialAttack)
        {
            List<CharacterBase> playerBoardCharacters = new();
            foreach (CardBase item in PlayerBoard.Cast<CardBase>())
            {
                playerBoardCharacters.Add((item.BindingContext as CharacterCardViewModel).Character);
            }
            List<CharacterBase> computerBoardCharacters = new();
            foreach (CardBase item in ComputerBoard.Cast<CardBase>())
            {
                computerBoardCharacters.Add((item.BindingContext as CharacterCardViewModel).Character);
            }

            myCharacter.SpecialAttack(computerBoardCharacters.ToArray(), playerBoardCharacters.ToArray(), enemyCharacter);
            players[0].SpecialPoints = 0;
        }

        #region Animation
        if (players[0].ChosenCard != null)
            (players[0].ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[0].ChosenCard = null;

        #region Animation
        if (players[0].TargetedCard != null)
            (players[0].TargetedCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[0].TargetedCard = null;

        players[0].AttackType = Player.AttackTypeEnum.Attack;

        players[0].TurnFaze = Player.TurnFazeEnum.EnemyTure;

        ComputerTurn();

        PlayerCards.IsVisible = true;
    }

    #endregion

    #region Computer turn

    private void ComputerTurn()
    {
        if (ComputerBoard.Children.Count > 0)
        {
            switch (new Random().Next(0, 2))
            {
                case 0:
                    new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1500, finished: (d, b) =>
                    {
                        players[1].ChosenCard = ComputerBoard[new Random().Next(0, ComputerBoard.Children.Count)] as CardBase;
                        players[1].HighlightChosenCard((d, b) => ComputerTargetEnemyCard());
                    });
                    break;

                case 1:
                    ToComputerThrowNewCard();
                    break;
            }

        }
        else ToComputerThrowNewCard();
    }

    private void ToComputerThrowNewCard()
    {
        if (players[1].DeckOfCards.Cards.Count < 1)
        {

            ClearComputerTurnData();
            return;
        }

        // czekanie na ruch
        new Animation((v) => { return; }).Commit(this, "Animation", 16, 1500, Easing.Linear, finished: (d, b) => ComputerThrowNewCard());
    }

    private void ComputerThrowNewCard()
    {
        players[1].ChosenCard = players[1].DeckOfCards.Cards.First();

        for (int i = 0; i < players[1].DeckOfCards.Cards.Count; i++)
        {
            if (!ComputerBoard.Contains(players[1].DeckOfCards.Cards[i]))
            {
                players[1].ChosenCard = players[1].DeckOfCards.Cards[i];
                break;
            }
        }

        if (!ComputerBoard.Contains(players[1].ChosenCard))
        {
            ComputerBoard.Children.Add(players[1].ChosenCard);
            players[1].ChosenCard.ToDestroy += players[1].RemoveCardFromBoard;
            players[1].ChosenCard.OnCardTaped += OnComputerCardClickedPlayerTargeted;
        }

        players[1].HighlightChosenCard((d,b)=>ComputerTargetEnemyCard());
    }

    private void ComputerTargetEnemyCard()
    {
        new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1500, finished: (d, b) =>
        {
            if (PlayerBoard.Children.Count == 0)
            {
                ClearComputerTurnData();
                return;
            }

            players[1].TargetedCard = PlayerBoard.Children[new Random().Next(0, PlayerBoard.Children.Count)] as CardBase;

            if (players[1].TargetedCard != null)
                new Animation(callback: v => (players[1].TargetedCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(v, 0, 0, 0.75),
                    start: 0,
                    end: 1).Commit(players[1].TargetedCard, "Animation", 16, highlightCardAnimationTime, finished: (d, b) => ComputerAttack());
        });
    }

    private void ComputerAttack()
    {
        // atak kończący turę
        new Animation((v) => { }).Commit(ComputerBoard, "Animation", 16, 1000, finished: (d, b) =>
        {
            CharacterBase computerCharacter = (players[1].ChosenCard.BindingContext as CharacterCardViewModel).Character;
            CharacterBase enemyCharacter = (players[1].TargetedCard.BindingContext as CharacterCardViewModel).Character;

            // Normal attack
            if (players[1].AttackType == Player.AttackTypeEnum.Attack)
            {
                computerCharacter.Attack(enemyCharacter);
                players[1].SpecialPoints++;
            }
            // Special attack
            else if (players[1].AttackType == Player.AttackTypeEnum.SpecialAttack)
            {
                List<CharacterBase> computerBoardCharacters = new();
                foreach (CardBase item in ComputerBoard.Cast<CardBase>())
                {
                    computerBoardCharacters.Add((item.BindingContext as CharacterCardViewModel).Character);
                }
                List<CharacterBase> playerBoardCharacters = new();
                foreach (CardBase item in ComputerBoard.Cast<CardBase>())
                {
                    playerBoardCharacters.Add((item.BindingContext as CharacterCardViewModel).Character);
                }

                computerCharacter.SpecialAttack(playerBoardCharacters.ToArray(), computerBoardCharacters.ToArray(), enemyCharacter);
                players[1].SpecialPoints = 0;
            }

            ClearComputerTurnData();
        });
    }

    private void ClearComputerTurnData()
    {
        #region Animation
        if (players[1].ChosenCard != null)
            (players[1].ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[1].ChosenCard = null;

        #region Animation
        if (players[1].TargetedCard != null)
            (players[1].TargetedCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Brush.Transparent;
        #endregion
        players[1].TargetedCard = null;

        players[0].TurnFaze = Player.TurnFazeEnum.SelectingPlayerCard;
    }

    #endregion

    #endregion

    // ---------------------------------------------- //

    #region Drag&Drop

    //private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    //{
    //    var d1 = e.Data.Properties["Card"];
    //    if (d1 is Card)
    //    {
    //        var card = d1 as Card;

    //        // For player1:
    //        if (player1.DeckOfCards.Cards.Contains(card))
    //        {
    //            if (!PlayerBoard.Contains(card))
    //            {
    //                PlayerThrowCard(card);
    //                PlayerTurn(card);
    //            }
    //        }
    //    }

    //    // Cofnięcie animacji:
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#66AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("Blue");
    //}

    //#region Animations

    //private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
    //{
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("DarkBlue");
    //}

    //private void DropGestureRecognizer_DragLeave(object sender, DragEventArgs e)
    //{
    //    PlayerBoardBorder.BackgroundColor = Color.FromHex("#66AAAAFF");
    //    PlayerBoardBorder.Stroke = Color.Parse("Blue");
    //}

    //#endregion

    #endregion
}