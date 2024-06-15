namespace Solution.Tests.Tests.Services.HeroServices;

public class HeroServiceBuilder : BaseServiceTestBuilder
{
    public IHeroService heroService;

    public HeroServiceBuilder(ApplicationDbContext dbContext) : base()
    {
        heroService = new HeroService(new HttpClient(), dbContext);
    }
}
