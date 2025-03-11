using Moq;
using Specifications;
using Specifications.Builders;

namespace Tests;

public class SpecificationBuilderTests
{
    public void IsValid_NoRulesDefined_ReturnsTrue<T>() where T : class
    {
        // Arrange
        var builder = new SpecificationBuilderBase<T>();
        var details = default(T);
        // Act
        var result = builder.IsValid(details);
        // Assert
        Assert.True(result);
    }

    public void IsValid_AllSpecificationsAndConditionsPass_ReturnsTrue<T>() where T : class
    {
        var mockSpec = new Mock<ISpecification<T>>();
        mockSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);
        var builder = new SpecificationBuilderBase<T>()
            .Rule(mockSpec.Object)
            .Rule(details => true);
        var details = default(T);

        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.True(result);
    }

    public void IsValid_AnySpecificationFails_ReturnsFalse<T>() where T : class
    {
        var passingSpec = new Mock<ISpecification<T>>();
        passingSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);

        var failingSpec = new Mock<ISpecification<T>>();
        failingSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(false);

        var builder = new SpecificationBuilderBase<T>()
            .Rule(passingSpec.Object)
            .Rule(failingSpec.Object)
            .Rule(details => true);
        var details = default(T);
        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.False(result);
    }

    public void IsValid_AnyConditionFails_ReturnsFalse<T>() where T : class
    {
        var mockSpec = new Mock<ISpecification<T>>();
        mockSpec.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);

        var builder = new SpecificationBuilderBase<T>()
            .Rule(mockSpec.Object)
            .Rule(details => false);

        var details = default(T);
        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.False(result);
    }

    public void IsValid_MultipleSpecificationsAndConditionsPass_ReturnsTrue<T>() where T : class
    {
        // Arrange
        var spec1 = new Mock<ISpecification<T>>();
        spec1.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);

        var spec2 = new Mock<ISpecification<T>>();
        spec2.Setup(s => s.IsSatisfiedBy(It.IsAny<T>())).Returns(true);

        var builder = new SpecificationBuilderBase<T>()
            .Rule(spec1.Object)
            .Rule(spec2.Object)
            .Rule(details => true)
            .Rule(details => true);
        var details = default(T);
        // Act
        var result = builder.IsValid(details);

        // Assert
        Assert.True(result);
    }
}