using CardActionsApi.Models;
using Specifications.Rules;

namespace CardActionsApi.SpecificationBuilder.Rules;

public class AnyOfTypes(CardType[] items) : IsSatisfiableEnum<CardType, CardDetails>(items)
{
    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardType);
    }
}