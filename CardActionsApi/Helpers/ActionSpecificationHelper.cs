﻿using CardActionsApi.Models;
using CardActionsApi.SpecificationBuilder;
using CardActionsApi.SpecificationBuilder.Rules;
using Specifications.Builders;

namespace CardActionsApi.Helpers;

public class ActionSpecificationHelper
{
    public static ISpecificationBuilder<CardDetails> IsAccessibleCardTypeAndAnyOfState(CardStatus[] statuses) =>
        new ActionSpecificationBuilder().Rule(x=> Enum.GetValues<CardType>().Contains(x.CardType)).Rule(new AnyOfStatuses(statuses));
    public static ISpecificationBuilder<CardDetails> IsAccessibleCardTypeAndAccessibleState() =>
        new ActionSpecificationBuilder().Rule(x => Enum.GetValues<CardType>().Contains(x.CardType)).Rule(x => Enum.GetValues<CardStatus>().Contains(x.CardStatus));
    public static ISpecificationBuilder<CardDetails> AnyOfCardTypeAndIsAccessibleState(CardType[] types) =>
        new ActionSpecificationBuilder().Rule(new AnyOfTypes(types)).Rule(x => Enum.GetValues<CardStatus>().Contains(x.CardStatus));
}