namespace Solution.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<ArenaEntity> Arenas { get; set; }

    public DbSet<HeroEntity> Heroes { get; set; }

    public DbSet<FightHistoryEntity> FightHistories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
