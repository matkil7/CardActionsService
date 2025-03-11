using CardActionsApi.Models;
using CardActionsApi.SpecificationBuilder;
using Moq;

namespace Tests;

public class ActionSpecificationBuilderTests
{
    [Fact]
    public void TestActionSpecificationBuilderBase()
    {
        SpecificationBuilderTestHelper.IsValid_NoRulesDefined_ReturnsTrue<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AllSpecificationsAndConditionsPass_ReturnsTrue<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AnySpecificationFails_ReturnsFalse<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_AnyConditionFails_ReturnsFalse<CardDetails>();
        SpecificationBuilderTestHelper.IsValid_MultipleSpecificationsAndConditionsPass_ReturnsTrue<CardDetails>();
    }

    [Fact]
    public void TestAction1Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
      public void TestAction2Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction3Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction4Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction5Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction6Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction7Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction8Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction9Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction10Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction11Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction12Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
    public void TestAction13Pass()
    {
        // Arrange
        var builder =  new ActionSpecificationBuilder();
        var details = new CardDetails(It.IsAny<string>(), It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());  
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }
}