namespace Solution.Tests.Tests.Services.ArenaServices;

public class ArenaServiceTests : BaseServiceTests
{
    private readonly ArenaServiceBuilder serviceBuilder;

    public ArenaServiceTests() : base()
    {
        serviceBuilder = new ArenaServiceBuilder(dbContext);
    }

    [Fact]
    public async Task ArenaFight_HasLogs()
    {
        var arena = await dbContext.Arenas.FirstAsync();

        FightHistory result = null;
        var exception = await Record.ExceptionAsync(async () =>
            result = await serviceBuilder.arenaService.FightAsync(arena.Id));

        Assert.Null(exception);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Log);
    }

    [Fact]
    public async Task ArenaFight_NotExistingArena_HasException()
    {
        FightHistory result = null;
        var exception = await Record.ExceptionAsync(async () =>
            result = await serviceBuilder.arenaService.FightAsync(2));

        Assert.NotNull(exception);
        Assert.Equal("Arena withe the provided id of 2 not exists", exception.Message);
    }
}
