namespace Solution.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(GlobalErrorResponse), (int)HttpStatusCode.BadRequest)]
public class HeroController(IHeroService heroService) : ControllerBase
{
    [Logging]
    [HttpPost("add")]
    [SwaggerOperation(OperationId = "addAsync", Description = "Create a new arena and adds the definied number of heroes in it.")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddHeroesToArenaAsync([Required][FromBody] AddHeroesToNewArenaRequest request)
    {
        return Ok(await heroService.AddHeroesToArenaAsync(request));
    }
}
