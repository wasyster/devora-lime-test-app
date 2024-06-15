namespace Solution.Core.Models.View;

public class SwordsMan : Hero
{
    public SwordsMan(): base(HeroType.SwordsMan, 120)
    {
    }

    public override void Attack(IHero enemy)
    {
        if (enemy is Cavalier cavalier)
        {
            return;
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
