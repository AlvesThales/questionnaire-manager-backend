using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuestionnaireManager.Application.Commands.CreateAnswer;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.UnitTests.Builders;

namespace QuestionnaireManager.UnitTests.Application;

[TestClass]
public class CreateAnswerHandlerTests
{
    private Mock<IQuestionnaireRepository> questionnaireRepository;
    private Mock<IQuestionRepository> questionRepository;

    private CreateAnswerHandler handler;

    [TestInitialize]
    public void Init()
    {
        questionnaireRepository = new Mock<IQuestionnaireRepository>();
        questionRepository = new Mock<IQuestionRepository>();

        handler = new CreateAnswerHandler(questionnaireRepository.Object, questionRepository.Object);
    }
    
    [TestMethod]
    public async Task HandleAsync_ShouldReturnFailureResult_WhenQuestionnaireNotFound()
    {
        var command = new CreateAnswerCommandBuilder()
            .WithQuestionnaireId(1)
            .WithQuestionId(2)
            .WithAnswerDescription("Answer description")
            .Build();
        
        questionnaireRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Questionnaire) null);
        
        var result = await handler.HandleAsync(command);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Questionnaire not found or contains no questions", result.ErrorMessage);
    }
    
    [TestMethod]
    public async Task HandleAsync_ShouldReturnFailureResult_WhenQuestionNotFound()
    {
        var questionnaire = new QuestionnaireBuilder()
            .WithTitle("test")
            .WithId(1)
            .WithMaxQuestions(5)
            .WithMaxAnswers(2)
            .Build();
        
        var command = new CreateAnswerCommandBuilder()
            .WithQuestionnaireId(1)
            .WithQuestionId(2)
            .WithAnswerDescription("Answer description")
            .Build();

        questionnaireRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(questionnaire);

        var result = await handler.HandleAsync(command);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Questionnaire not found or contains no questions", result.ErrorMessage);
    }
    
    [TestMethod]
    public async Task HandleAsync_ShouldReturnFailureResult_WhenAnswersLimitReached()
    {
        var questionnaire = new QuestionnaireBuilder()
            .WithTitle("test")
            .WithId(1)
            .WithMaxQuestions(5)
            .WithMaxAnswers(0)
            .WithQuestion(q => q
                .WithId(2)
                .WithDescription("Question description")
                .WithAnswers(new List<Answer>()))
            .Build();
        
        var command = new CreateAnswerCommandBuilder()
            .WithQuestionnaireId(1)
            .WithQuestionId(2)
            .WithAnswerDescription("Answer description")
            .Build();
        
        questionnaireRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(questionnaire);

        var result = await handler.HandleAsync(command);
        
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Answers limit has been reached", result.ErrorMessage);
    }
    
    [TestMethod]
    public async Task HandleAsync_ShouldReturnOkResult_WhenAnswerIsCreated()
    {
        var questionnaire = new QuestionnaireBuilder()
            .WithTitle("test")
            .WithId(1)
            .WithMaxQuestions(5)
            .WithMaxAnswers(1)
            .WithQuestion(q => q
                .WithId(2)
                .WithDescription("Question description")
                .WithAnswers(new List<Answer>()))
            .Build();
        
        var command = new CreateAnswerCommandBuilder()
            .WithQuestionnaireId(1)
            .WithQuestionId(2)
            .WithAnswerDescription("Answer description")
            .Build();

        questionnaireRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(questionnaire);

        var result = await handler.HandleAsync(command);
        
        Assert.IsTrue(result.Success);
    }
}