using CardActionsApi.Models;
using Specifications.Builders;

namespace CardActionsApi.Providers.Actions;

public class ActionsSpecificationsProvider : ISpecificationsProvider<CardDetails>
{
    public Dictionary<int, ISpecificationBuilder<CardDetails>> GetDefinitions()
    {
        throw new NotImplementedException();
    }
}