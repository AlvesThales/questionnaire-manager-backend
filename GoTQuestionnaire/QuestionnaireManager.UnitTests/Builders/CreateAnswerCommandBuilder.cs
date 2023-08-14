using QuestionnaireManager.Application.Commands.CreateAnswer;

namespace QuestionnaireManager.UnitTests.Builders;

public class CreateAnswerCommandBuilder
{
    private int _questionnaireId;
    private int _questionId;
    private string _answerDescription;

    public CreateAnswerCommandBuilder WithQuestionnaireId(int questionnaireId)
    {
        _questionnaireId = questionnaireId;
        return this;
    }

    public CreateAnswerCommandBuilder WithQuestionId(int questionId)
    {
        _questionId = questionId;
        return this;
    }

    public CreateAnswerCommandBuilder WithAnswerDescription(string answerDescription)
    {
        _answerDescription = answerDescription;
        return this;
    }

    public CreateAnswerCommand Build()
    {
        return new CreateAnswerCommand(_questionnaireId, _questionId, _answerDescription);
    }
}