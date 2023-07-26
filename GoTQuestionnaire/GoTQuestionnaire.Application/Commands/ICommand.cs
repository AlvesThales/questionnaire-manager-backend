using GoTQuestionnaire.Infrastructure.Utils;

namespace GoTQuestionnaire.Application.Commands;

public interface ICommand
{
}

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command);
}