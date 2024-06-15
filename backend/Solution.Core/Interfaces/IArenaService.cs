namespace Solution.Core.Interfaces;

public interface IArenaService
{
    Task<FightHistory> FightAsync(int arenaId);
    Task<PagedResponse<ArenaListItem>> GetArenaListAsync(int page);
}
