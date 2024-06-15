namespace Solution.Services.Factories;

public static class HeroFactory
{
    public static IHero Create(HeroType heroType) => heroType switch
    {
        HeroType.Archer => new Archer(),
        HeroType.SwordsMan => new SwordsMan(),
        HeroType.Cavalier => new Cavalier()
    };
}
