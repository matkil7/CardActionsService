using CardActionsApi.Models;
using Specifications.Rules;

namespace CardActionsApi.SpecificationBuilder.Rules;

public class IsAccessibleType() : IsSatisfiableEnum<CardType, CardDetails>(Enum.GetValues<CardType>())
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardType);
    }
}