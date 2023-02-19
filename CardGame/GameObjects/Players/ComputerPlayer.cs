using CardGame.ViewModels;

namespace CardGame.GameObjects
{
    internal class ComputerPlayer : Player
    {
        // wybiera rodzaj ataku i aktywuje animacje wybrania karty
        public override void HighlightChosenCard(Action<double, bool> finished = null)
        {
            if (this.SpecialPoints >= 3)
                this.AttackType = (Player.AttackTypeEnum)new Random().Next(0, 2);
            else
                this.AttackType = Player.AttackTypeEnum.Attack;

            switch (this.AttackType)
            {
                case Player.AttackTypeEnum.Attack:
                    if (this.ChosenCard != null)
                        new Animation(callback: v => (this.ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(0, 0, 0, v),
                        start: 0,
                        end: 0.75).Commit(this.ChosenCard, "Animation", 16, highlightCardAnimationTime, finished: finished);
                    break;

                case Player.AttackTypeEnum.SpecialAttack:
                    if (this.ChosenCard != null)
                        new Animation(callback: v => (this.ChosenCard.BindingContext as CharacterCardViewModel).Character.AuraBrush = Color.FromRgba(v, v, 0, v),
                        start: 0,
                        end: 0.75).Commit(this.ChosenCard, "Animation", 16, highlightCardAnimationTime, finished: finished);
                    break;
            }

        }
    }
}
