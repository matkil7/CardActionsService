using CardActionsApi.Helpers;
using CardActionsApi.Models;
using CardActionsApi.SpecificationBuilder;
using CardActionsApi.SpecificationBuilder.Rules;
using Moq;

namespace Tests;

public class ActionSpecificationBuilderTests
{
    public static IEnumerable<object[]> Action1TestCase =>
        new List<object[]>
        {
            new object[]
            {
                ActionSpecificationHelper.IsAccessibleCardTypeAndAnyOfState([CardStatus.Active]),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid], [CardStatus.Active], null),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Inactive, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired
                ], null)
            }
        };

    public static IEnumerable<object[]> Action2TestCase =>
        new List<object[]>
        {
            new object[]
            {
                ActionSpecificationHelper.IsAccessibleCardTypeAndAnyOfState([CardStatus.Inactive]),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid], [CardStatus.Inactive], null),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired
                ], null)
            }
        };

    public static IEnumerable<object[]> Action3TestCase =>
        new List<object[]>
        {
            new object[]
            {
                ActionSpecificationHelper.IsAccessibleCardTypeAndAccessibleState(),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired, CardStatus.Inactive
                ], null),
                Enumerable.Empty<CardDetails>()
            }
        };

    public static IEnumerable<object[]> Action4TestCase =>
        new List<object[]>
        {
            new object[]
            {
                ActionSpecificationHelper.IsAccessibleCardTypeAndAccessibleState(),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired, CardStatus.Inactive
                ], null),
                Enumerable.Empty<CardDetails>()
            }
        };

    public static IEnumerable<object[]> Action5TestCase =>
        new List<object[]>
        {
            new object[]
            {
                ActionSpecificationHelper.AnyOfCardTypeAndIsAccessibleState([CardType.Credit]),
                PrepareCards([CardType.Credit],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired, CardStatus.Inactive
                ], null),
                PrepareCards([CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired, CardStatus.Inactive
                ], null)
            }
        };

    public static IEnumerable<object[]> Action6TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(x => x.CardType == CardType.Credit).Rule(x =>
                    x.IsPinSet && CardStatusHelper.OrderedInactiveActiveBlocked.Contains(x.CardStatus)),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Ordered, CardStatus.Active, CardStatus.Blocked, CardStatus.Inactive], true),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                [
                    CardStatus.Ordered, CardStatus.Active, CardStatus.Restricted, CardStatus.Blocked,
                    CardStatus.Expired, CardStatus.Inactive
                ], false)
            }
        };
    
    public static IEnumerable<object[]> Action7TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(x =>
                    (!x.IsPinSet && CardStatusHelper.OrderedInactiveActive.Contains(x.CardStatus)) ||
                    (x.IsPinSet && x.CardStatus == CardStatus.Blocked)),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Ordered, CardStatus.Active, CardStatus.Inactive], false,
                    new[] { new CardDetails(It.IsAny<string>(), CardType.Credit, CardStatus.Blocked, true) }),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Restricted, CardStatus.Blocked], false)
            }
        };

    public static IEnumerable<object[]> Action8TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleStatus())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActiveBlocked)),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    CardStatusHelper.OrderedInactiveActiveBlocked, false),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Restricted, CardStatus.Expired, CardStatus.Closed], false)
            }
        };

    public static IEnumerable<object[]> Action9TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new IsAccessibleStatus()),
                PrepareCards(Enum.GetValues<CardType>(), Enum.GetValues<CardStatus>(), true),
                Enumerable.Empty<CardDetails>()
            }
        };

    public static IEnumerable<object[]> Action10TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActive)),
                PrepareCards(Enum.GetValues<CardType>(), CardStatusHelper.OrderedInactiveActive, true),
                PrepareCards(Enum.GetValues<CardType>(),
                    [CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired], false)
            }
        };

    public static IEnumerable<object[]> Action11TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new AnyOfStatuses(
                    CardStatusHelper.InactiveActive
                )),
                PrepareCards(Enum.GetValues<CardType>(), [CardStatus.Active, CardStatus.Inactive], true),
                PrepareCards(Enum.GetValues<CardType>(),
                    [CardStatus.Ordered, CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired], false)
            }
        };

    public static IEnumerable<object[]> Action12TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType()).Rule(new AnyOfStatuses(
                    CardStatusHelper.OrderedInactiveActive
                )),
                PrepareCards(Enum.GetValues<CardType>(), CardStatusHelper.OrderedInactiveActive, true),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired], false)
            }
        };

    public static IEnumerable<object[]> Action13TestCase =>
        new List<object[]>
        {
            new object[]
            {
                new ActionSpecificationBuilder().Rule(new IsAccessibleType())
                    .Rule(new AnyOfStatuses(CardStatusHelper.OrderedInactiveActive)),
                PrepareCards(Enum.GetValues<CardType>(), CardStatusHelper.OrderedInactiveActive, true),
                PrepareCards([CardType.Credit, CardType.Debit, CardType.Prepaid],
                    [CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired], false)
            }
        };

    [Fact]
    public void TestActionSpecificationBuilderBase()
    {
        SpecificationBuilderTestHelper.IsValid_NoRulesDefined_ReturnsTrue<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AllSpecificationsAndConditionsPass_ReturnsTrue<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AnySpecificationFails_ReturnsFalse<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AnyConditionFails_ReturnsFalse<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_MultipleSpecificationsAndConditionsPass_ReturnsTrue<CardDetails>();
    }


    [Theory]
    [MemberData(nameof(Action1TestCase))]
    [MemberData(nameof(Action2TestCase))]
    [MemberData(nameof(Action3TestCase))]
    [MemberData(nameof(Action4TestCase))]
    [MemberData(nameof(Action5TestCase))]
    [MemberData(nameof(Action6TestCase))]
    [MemberData(nameof(Action7TestCase))]
    [MemberData(nameof(Action8TestCase))]
    [MemberData(nameof(Action9TestCase))]
    [MemberData(nameof(Action10TestCase))]
    [MemberData(nameof(Action11TestCase))]
    [MemberData(nameof(Action12TestCase))]
    [MemberData(nameof(Action13TestCase))]
    public void CardsPassed(ActionSpecificationBuilder builder, List<CardDetails> toSuccedDetails,
        IEnumerable<CardDetails> toFailDetails)
    {
        foreach (var toPass in toSuccedDetails)
        {
            var result = builder.IsValid(toPass);
            Assert.True(result);
        }

        foreach (var toFail in toFailDetails)
        {
            var result = builder.IsValid(toFail);
            Assert.False(result);
        }
    }

    public static List<CardDetails> PrepareCards(IEnumerable<CardType> cardTypes, IEnumerable<CardStatus> cardStatuses,
        bool? IsPinSet, IEnumerable<CardDetails>? cardDetails = null)
    {
        var values = cardTypes
            .SelectMany(x => cardStatuses,
                (x, y) => new CardDetails(It.IsAny<string>(), x, y,
                    IsPinSet == null ? It.IsAny<bool>() : IsPinSet.Value)).ToList();
        if (cardDetails != null) values.AddRange(cardDetails);
        return values.ToList();
    }
}