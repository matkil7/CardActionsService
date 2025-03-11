using CardActionsApi.Models;
using Specifications.Rules;

namespace CardActionsApi.SpecificationBuilder.Rules;

public class AnyOfStatuses : IsSatisfiableEnum<CardStatus, CardDetails> {
    public AnyOfStatuses(CardStatus[] items) : base(items)
    {
    }

    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardStatus);
    }
}