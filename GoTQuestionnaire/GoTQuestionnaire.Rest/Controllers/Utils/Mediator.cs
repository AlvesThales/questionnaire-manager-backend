namespace GoTQuestionnaire.Rest.Controllers.Utils;

public class Mediator
{
    private readonly IServiceProvider _provider;

    public Mediator(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<Result> DispatchAsync(ICommand command)
    {
        var type = typeof(ICommandHandler<>);
        Type[] typeArgs = { command.GetType() };
        Type handlerType = type.MakeGenericType(typeArgs);
        dynamic handler = _provider.GetRequiredService(handlerType);

        Result result = await handler.HandleAsync((dynamic)command);

        return result;
    }

    public async Task<T> DispatchAsync<T>(IQuery<T> query)
    {
        var type = typeof(IQueryHandlerAsync<,>);
        Type[] typeArgs = { query.GetType(), typeof(T) };
        Type handlerType = type.MakeGenericType(typeArgs);

        dynamic handler = _provider.GetRequiredService(handlerType);
        T result = await handler.HandleAsync((dynamic)query);
        return result;
    }
}

public interface IMediator
{
    Task<Result> DispatchAsync(ICommand command);
    
    Task<T> DispatchAsync<T>(IQuery<T> query);
}