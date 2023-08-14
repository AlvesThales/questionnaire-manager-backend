using System.Collections;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.UnitTests.Builders;

public class QuestionBuilder
{
    private int _id;
    private string _description;
    private ICollection<Answer> _answers;

    public QuestionBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public QuestionBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }
    
    public QuestionBuilder WithAnswers(ICollection<Answer> answers)
    {
        _answers = answers;
        return this;
    }

    public Question Build()
    {
        return new Question(_description)
        {
            Id = _id,
            Answers = _answers
        };
    }
}
