namespace Solution.Services;

public class HeroService(HttpClient httpClient, ApplicationDbContext dbContext) : IHeroService
{
    private Random random = new Random();

    public async Task<AddHeroesToNewArenaResponse> AddHeroesToArenaAsync(AddHeroesToNewArenaRequest paramsData)
    {
        var heroes = new List<HeroEntity>();
        var heroesTypes = EnumToDictionary<HeroType>();

        var arenaId = await CreateNewArenaAsync();

        while (paramsData.NumberOfHeros != 0)
        {
            var heroType = heroesTypes[random.Next(0, heroesTypes.Count)];
            var hero = HeroFactory.Create(heroType);

            heroes.Add(hero.ToEntity(arenaId));

            paramsData.NumberOfHeros--;

            //this is just to get more random heroes
            //if we use this aproach in the real life, then the request will time out
            //for that then we need to use some kind of background service
            //but if I had more time this function shuld be some kind of background process
            await Task.Delay(100);
        }

        //bulk insert would be nicer, but it throws me an error in test, I requires time to fix
        await dbContext.Heroes.AddRangeAsync(heroes);
        await dbContext.SaveChangesAsync();

        return new AddHeroesToNewArenaResponse
        {
            ArenaId = arenaId
        };
    }

    private async Task<int> CreateNewArenaAsync()
    {
        var fakeNameGeneratorResponse = await httpClient.GetFromJsonAsync<FakeNameGeneratorResponse>("https://api.namefake.com/");
        var arena = new ArenaEntity
        {
            Name = fakeNameGeneratorResponse.Name ?? GetRandomArenaName()
        };
        await dbContext.Arenas.AddAsync(arena);
        await dbContext.SaveChangesAsync();

        return arena.Id;
    }

    private Dictionary<int, TEnum> EnumToDictionary<TEnum>() where TEnum : struct
    {
        var result = Enum.GetValues(typeof(TEnum))
                         .Cast<TEnum>()
                         .Select(x => new { Key = Convert.ToInt32(x), Value = x })
                         .ToDictionary(x => x.Key, x => x.Value);

        return result;
    }

    private string GetRandomArenaName()
    {
        var buffer = new byte[5];
        new Random().NextBytes(buffer);
        return string.Join("", buffer.Select(b => b.ToString("X2")));
    }
}
