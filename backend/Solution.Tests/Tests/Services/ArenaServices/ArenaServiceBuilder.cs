namespace Solution.Tests.Tests.Services.ArenaServices;

public class ArenaServiceBuilder : BaseServiceTestBuilder
{
    public IArenaService arenaService;

    public ArenaServiceBuilder(ApplicationDbContext dbContext) : base()
    {
        arenaService = new ArenaService(dbContext);
    }
}
