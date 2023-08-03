namespace Questionnaire.Infrastructure.Exceptions;

public class QuestionsLimitReachedException : Exception
{
    public QuestionsLimitReachedException() : base("Questions limit has been reached. Can't create more questions.")
    {
    }
}