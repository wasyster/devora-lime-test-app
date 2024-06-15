namespace Solution.Core.Models.View;

public abstract class Hero : IHero
{
    public int Id { get; set; }

    public int Health { get; set; }

    public HeroType Type { get; }

    protected Random rnd => new Random();

    public bool IsDead { get; set; }

    public bool IsWinner { get; set; }

    public Hero(HeroType heroType, int health)
    {
        this.Health = health;
        this.Type = heroType;
    }

    public virtual void ToEntity(HeroEntity hero)
    {
        hero.Id = Id;
        hero.Health = this.Health;
        hero.Type = this.Type;
        hero.IsDead = this.IsDead;
        hero.IsWinner = this.IsWinner;
    }

    public virtual HeroEntity ToEntity(int arenaId)
    {
        return new HeroEntity
        {
            Id = this.Id,
            Health = this.Health,
            Type = this.Type,
            IsDead = this.IsDead,
            IsWinner = this.IsWinner,
            ArenaId = arenaId
        };
    }

    public abstract void Attack(IHero enemy);
}
