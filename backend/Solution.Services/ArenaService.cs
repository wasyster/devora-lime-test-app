namespace Solution.Services;

public class ArenaService(ApplicationDbContext dbContext) : IArenaService
{
    private const int itemPerPage = 10;
    private Random random = new Random();

    public async Task<PagedResponse<ArenaListItem>> GetArenaListAsync(int page)
    {
        page = page < 0 ? 0 : page - 1;

        var arenaListItems = await dbContext.Arenas.AsNoTracking()
                                                   .Include(x => x.Heroes)
                                                   .Select(x => new ArenaListItem
                                                   { 
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        NumberOfHeroes = x.Heroes.Count,
                                                        Winner = x.FightHistories.Any(x => x.ArenaId == x.ArenaId) ?
                                                                 x.Heroes.First(x => !x.IsDead).Type.ToString() : string.Empty,
                                                        Logs = x.FightHistories.Where(x => x.ArenaId == x.ArenaId).Select(x => x.Log).ToList()
                                                   })
                                                   .Skip(page * itemPerPage)
                                                   .Take(itemPerPage)
                                                   .ToListAsync();

        return new PagedResponse<ArenaListItem>
        { 
            Count = await dbContext.Arenas.CountAsync(),
            Items = arenaListItems
        };
    }

    public async Task<FightHistory> FightAsync(int arenaId)
    {
        var round = 1;

        var arena = await dbContext.Arenas.FirstOrDefaultAsync(x => x.Id == arenaId);

        if(arena is null)
        {
            throw new Exception($"Arena withe the provided id of {arenaId} not exists");
        }

        var heros = await dbContext.Heroes.Where(x => x.ArenaId == arenaId)
                                          .Select(x => HeroFactory.Create(x.Type))
                                          .ToListAsync();
        
        do
        {
            var fightingHeros = heros.OrderBy(x => random.Next())
                                     .Take(2);


            var attacker = fightingHeros.First();
            var enemy = fightingHeros.Last();

            await AddLogHistoryAsync(arenaId, $"round-{round.ToString().PadLeft(4, '0')}");
            await AddLogHistoryAsync(arenaId, $"{attacker.Type.ToString().ToUpper()} attacked {enemy.Type.ToString().ToUpper()}");

            attacker.Attack(enemy);
            ReCalculateFighterHealth(attacker);
            ReCalculateFighterHealth(enemy);

            await AddLogHistoryAsync(arenaId, $"{attacker.Type.ToString().ToUpper()} health: {attacker.Health}");
            await AddLogHistoryAsync(arenaId, $"{enemy.Type.ToString().ToUpper()} health: {enemy.Health}");

            if (attacker.IsDead)
            {
                await AddLogHistoryAsync(arenaId, $"{attacker.Type.ToString().ToUpper()} died");
            }

            if (enemy.IsDead)
            {
                await AddLogHistoryAsync(arenaId, $"{enemy.Type.ToString().ToUpper()} died");
            }

            foreach (var hero in heros)
            {
                if(hero.Id != attacker.Id && hero.Id != enemy.Id)
                {
                    hero.Health += 10;
                }
            }

            heros = heros.Where(x => !x.IsDead)
                         .ToList();
            
            await dbContext.SaveChangesAsync();
            round++;
        }
        while (heros.Count(x => !x.IsDead) != 1);

        var winner = heros.FirstOrDefault(x => !x.IsDead);

        dbContext.Heroes.Update(winner.ToEntity(arenaId));

        return new FightHistory
        { 
            Log = await dbContext.FightHistories.Where(x => x.ArenaId == arenaId)
                                                .Select(x => x.Log)
                                                .ToListAsync(),
        };
    }

    private void ReCalculateFighterHealth(IHero hero)
    {
        var baseHealth = GetHeroBaseHelth(hero.Type);
        var healthLimit = (int)(baseHealth / 4);
        
        hero.Health = (int)(hero.Health / 2);

        if(hero.Health < healthLimit)
        {
            hero.IsDead = true;
        }
    }

    private int GetHeroBaseHelth(HeroType heroType) => heroType switch
    {
        HeroType.Archer => 100,
        HeroType.SwordsMan => 120,
        HeroType.Cavalier => 150
    };

    private async Task AddLogHistoryAsync(int arenaId, string logText)
    {
        await dbContext.FightHistories.AddAsync(new FightHistoryEntity
        {
            ArenaId = arenaId,
            Log = logText
        });
    }
}
