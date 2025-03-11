namespace Specifications.Builders;

public class SpecificationBuilderBase<T> : ISpecificationBuilder<T> where T : class
{
    private readonly List<Func<T, bool>> _conditions = [];
    private readonly List<ISpecification<T>> _specifications = [];

    public ISpecificationBuilder<T> Rule(ISpecification<T> spec)
    {
        _specifications.Add(spec);
        return this;
    }

    public ISpecificationBuilder<T> Rule(Func<T, bool> condition)
    {
        _conditions.Add(condition);
        return this;
    }

    public bool IsValid(T details)
    {
        return _specifications.All(spec =>
            spec.IsSatisfiedBy(details) && _conditions.All(condition => condition(details)));
    }
}