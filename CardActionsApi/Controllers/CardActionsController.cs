using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CardActionsApi.Services.Actions;
using Microsoft.AspNetCore.Mvc;
using Results;

namespace CardActionsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardActionsController : ControllerBase
{
    private readonly IActionService _actionService;

    public CardActionsController(IActionService actionService)
    {
        _actionService = actionService;
    }

    [HttpGet(Name = "GetCardActions")]
    
    public async Task<IActionResult>  Get([Required] string userId, [Required]string cardNumber)
    {
        var actions = await _actionService.GetCardActions(userId, cardNumber);
        if (actions.IsSuccess)
        {
            return Ok(JsonSerializer.Serialize(actions.Value));
        }
        return NotFound();
    }
}