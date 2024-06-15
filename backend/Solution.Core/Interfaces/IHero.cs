namespace Solution.Core.Interfaces;

public interface IHero
{
    int Id { get; set; }

    int Health { get; set; }

    bool IsDead { get; set; }

    public HeroType Type { get; }

    void ToEntity(HeroEntity hero);

    HeroEntity ToEntity(int arenaId);

    void Attack(IHero enemy);
}
