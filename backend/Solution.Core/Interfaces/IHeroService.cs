namespace Solution.Core.Interfaces;

public interface IHeroService
{
    Task<AddHeroesToNewArenaResponse> AddHeroesToArenaAsync(AddHeroesToNewArenaRequest paramsData);
}
