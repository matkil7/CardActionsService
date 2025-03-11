using Results;

namespace CardActionsApi.Services.Actions;

public interface IActionService
{
    Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber);
}