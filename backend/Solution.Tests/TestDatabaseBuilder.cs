namespace Solution.Tests;

public class TestDatabaseBuilder
{
    private ApplicationDbContext dbContext;

    public ApplicationDbContext CreateDbContext()
    {
        dbContext ??= new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        return dbContext;
    }

    public async Task SetupTestDatabaseWithDefaultsAsync()
    {
        var arena = new ArenaEntity
        {
            Name = "Test Arena"
        };

        await dbContext.Arenas.AddAsync(arena);
        await dbContext.SaveChangesAsync();

        await dbContext.Heroes.AddAsync(new HeroEntity
        { 
            ArenaId = arena.Id,
            Health = 100,
            IsDead = false,
            IsWinner = false,
            Type = HeroType.Archer
        });

        await dbContext.Heroes.AddAsync(new HeroEntity
        {
            ArenaId = arena.Id,
            Health = 120,
            IsDead = false,
            IsWinner = false,
            Type = HeroType.SwordsMan
        });

        await dbContext.Heroes.AddAsync(new HeroEntity
        {
            ArenaId = arena.Id,
            Health = 150,
            IsDead = false,
            IsWinner = false,
            Type = HeroType.Cavalier
        });

        await dbContext.SaveChangesAsync();
    }
}
