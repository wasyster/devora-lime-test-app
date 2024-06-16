namespace Solution.Tests.Tests.Heros;

public class ArcherTests
{
    [Fact]
    public void Archer_vs_SwordsMan_SwordsManDies()
    {
        var archer = new Archer();
        var swordsMan = new SwordsMan();

        archer.Attack(swordsMan);

        Assert.True(swordsMan.IsDead);
        Assert.Equal(0, swordsMan.Health);

        Assert.True(archer.IsWinner);
    }

    [Fact]
    public void Archer_vs_Archer_DefendingArcherDies()
    {
        var archer = new Archer();
        var defendingArcher = new Archer();

        archer.Attack(defendingArcher);

        Assert.True(defendingArcher.IsDead);
        Assert.Equal(0, defendingArcher.Health);

        Assert.True(archer.IsWinner);
    }

    [Fact]
    public void Archer_vs_Cavalier()
    {
        var archer = new Archer();
        var cavalier = new Cavalier();

        archer.Attack(cavalier);

        Assert.Multiple(
            () => Assert.True(archer.IsWinner),
            () => Assert.True(cavalier.IsDead)
        );
    }
}
