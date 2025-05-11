namespace FinanceTracker.Api.Common.Base;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}
