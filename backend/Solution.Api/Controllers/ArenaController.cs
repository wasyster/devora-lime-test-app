namespace Solution.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(GlobalErrorResponse), (int)HttpStatusCode.BadRequest)]
public class ArenaController(IArenaService arenaService): ControllerBase
{
    [Logging]
    [HttpGet("page/{pageNumber}")]
    [SwaggerOperation(OperationId = "addAsync", Description = "Returns paged arena list with number of heroes.")]
    [ProducesResponseType(typeof(PagedResponse<ArenaListItem>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddHeroesToArenaAsync([Required][FromRoute] int pageNumber)
    {
        return Ok(await arenaService.GetArenaListAsync(pageNumber));
    }

    [Logging]
    [HttpGet("fight/{arenaId}")]
    [SwaggerOperation(OperationId = "fightAsync", Description = "Starts the arena tournament.")]
    [ProducesResponseType(typeof(FightHistory), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> FightAsync([Required][FromRoute] int arenaId)
    {
        return Ok(await arenaService.FightAsync(arenaId));
    }
}
