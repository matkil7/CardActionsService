using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Services.Card;
using Results;
using Specifications.Builders;

namespace CardActionsApi.Services.Actions;

public class ActionService : IActionService
{
    private readonly ICardService _cardService;
    private Dictionary<int, ISpecificationBuilder<CardDetails>> _specifications = new();
    private ILogger<ActionService> _logger;

    public ActionService(ISpecificationsProvider<CardDetails> actionDefinitionProvider, ICardService cardService,ILogger<ActionService> logger)
    {
        InitializeSpecifications(actionDefinitionProvider.GetDefinitions());
        _cardService = cardService;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<string>>> GetCardActions(string userId, string cardNumber)
    {
        try
        {
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
            if (cardDetails == null) return Result<IEnumerable<string>>.Failure("No card found");

            return Result<IEnumerable<string>>.Success(GetActions(cardDetails));
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message); 
            return Result<IEnumerable<string>>.Failure("Unexpected error");
        }
    }

    private void InitializeSpecifications(Dictionary<int, ISpecificationBuilder<CardDetails>>? specifications)
    {
        if (specifications != null && specifications.Any()) _specifications = specifications;
    }

    private IEnumerable<string> GetActions(CardDetails cardDetails)
    {
        return _specifications.Any()
            ? _specifications
                .Where(rule => rule.Value.IsValid(cardDetails))
                .Select(rule => $"ACTION{rule.Key}")
                .ToList()
            : Enumerable.Empty<string>();
    }
}