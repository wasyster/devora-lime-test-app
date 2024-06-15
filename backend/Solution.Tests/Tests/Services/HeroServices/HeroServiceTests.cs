namespace Solution.Tests.Tests.Services.HeroServices;

public class HeroServiceTests : BaseServiceTests
{
    private readonly HeroServiceBuilder serviceBuilder;

    public HeroServiceTests() : base()
    {
        serviceBuilder = new HeroServiceBuilder(dbContext);
    }

    [Fact]
    public async Task Add_10_Heroes_To_ArenaAsyncTest_Success()
    {
        var numberOfHeroesToAdd = 10;

        AddHeroesToNewArenaResponse result = null;
        var exception = await Record.ExceptionAsync(async () =>
            result = await serviceBuilder.heroService.AddHeroesToArenaAsync(new AddHeroesToNewArenaRequest
            { 
                NumberOfHeros = numberOfHeroesToAdd
            }));
        
        Assert.NotNull(result);
        Assert.NotEqual(0, result.ArenaId);

        var numberOfHeroes = await dbContext.Heroes.CountAsync();
        Assert.Null(exception);
        Assert.Equal(numberOfHeroesToAdd + NUMBER_OF_PREDEFINIED_HEROS, numberOfHeroes);
    }
}
