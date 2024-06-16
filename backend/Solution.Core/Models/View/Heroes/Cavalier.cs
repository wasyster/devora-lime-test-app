namespace Solution.Core.Models.View.Heroes;

public class Cavalier : Hero
{
    public Cavalier() : base(HeroType.Cavalier, 150)
    {
    }

    public override void Attack(IHero enemy)
    {
        if (enemy is Cavalier cavalier)
        {
            cavalier.IsDead = true;
            cavalier.Health = 0;

            this.IsWinner = true;
        }
        else if (enemy is SwordsMan swordsMan)
        {
            IsDead = true;
            Health = 0;

            swordsMan.IsWinner = true;
        }
        else if (enemy is Archer archer)
        {
            archer.IsDead = true;
            archer.Health = 0;

            this.IsWinner = true;
        }
    }
}
