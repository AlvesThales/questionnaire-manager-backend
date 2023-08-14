using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.UnitTests.Builders;

public class QuestionnaireBuilder
{
    private string _title;
    private int _id;
    private int _maxQuestions;
    private int _maxAnswers;
    private List<Question> _questions;

    public QuestionnaireBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public QuestionnaireBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public QuestionnaireBuilder WithMaxQuestions(int maxQuestions)
    {
        _maxQuestions = maxQuestions;
        return this;
    }
    
    public QuestionnaireBuilder WithMaxAnswers(int maxAnswers)
    {
        _maxAnswers = maxAnswers;
        return this;
    }

    public QuestionnaireBuilder WithQuestion(Action<QuestionBuilder> configureQuestion)
    {
        var questionBuilder = new QuestionBuilder();
        configureQuestion(questionBuilder);
        var question = questionBuilder.Build();
        _questions ??= new List<Question>();
        _questions.Add(question);
        return this;
    }

    public Questionnaire Build()
    {
        return new Questionnaire(_title, _maxAnswers, _maxQuestions)
        {
            Questions = _questions
        };
    }
}