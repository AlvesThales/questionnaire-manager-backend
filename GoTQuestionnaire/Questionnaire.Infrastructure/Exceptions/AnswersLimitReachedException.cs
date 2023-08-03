namespace Questionnaire.Infrastructure.Exceptions;

public class AnswersLimitReachedException : Exception
{
    public AnswersLimitReachedException() : base("Answers limit has been reached. Can't create more answers for this question.")
    {
    }
}