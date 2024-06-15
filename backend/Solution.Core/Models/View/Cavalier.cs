namespace Solution.Core.Models.View;

public class Cavalier: Hero
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
        }
        else if (enemy is SwordsMan swordsMan)
        {
            this.IsDead = true;
            this.Health = 0;
        }
        else if (enemy is Archer archer)
        {
            archer.IsDead = true;
            archer.Health = 0;
        }
    }
}
