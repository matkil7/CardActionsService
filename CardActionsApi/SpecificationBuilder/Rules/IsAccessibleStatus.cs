using CardActionsApi.Models;
using Specifications.Rules;

namespace CardActionsApi.SpecificationBuilder.Rules;

public class IsAccessibleStatus() : IsSatisfiableEnum<CardStatus, CardDetails>(Enum.GetValues<CardStatus>())
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardStatus);
    }
}