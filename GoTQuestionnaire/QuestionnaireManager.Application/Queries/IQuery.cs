namespace QuestionnaireManager.Application.Queries;

public interface IQuery<TResult>
{
}
    
public interface IQueryHandlerAsync<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}