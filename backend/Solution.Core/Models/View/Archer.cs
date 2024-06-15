namespace Solution.Core.Models.View;

public class Archer: Hero
{
    public Archer():base(HeroType.Cavalier, 100)
    {
    }

    public override void Attack(IHero enemy)
    {
        if(enemy is Cavalier cavalier)
        {
            cavalier.IsDead = (100 - rnd.Next(0, 101) <= 40);
            if(cavalier.IsDead)
            {
                cavalier.Health = 0;
            }
        }
        else if (enemy is SwordsMan swordsMan)
        {
            swordsMan.IsDead = true;
            swordsMan.Health = 0;
        }
        else if (enemy is Archer archer)
        {
            archer.IsDead = true;
            archer.Health = 0;
        }
    }
}
