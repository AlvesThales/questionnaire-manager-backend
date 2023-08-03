using Questionnaire.Infrastructure.Utils;

namespace Questionnaire.Application.Commands;

public interface ICommand
{
}

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task<Result> HandleAsync(TCommand command);
}