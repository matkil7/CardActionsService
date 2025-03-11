using Microsoft.AspNetCore.Mvc;

namespace CardActionsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardActionsController : ControllerBase
{
    public CardActionsController()
    {
    }

    [HttpGet(Name = "GetCardActions")]
    public IEnumerable<string> Get()
    {
        return [];
    }
}