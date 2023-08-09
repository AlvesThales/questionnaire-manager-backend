using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Rest.Model.Response;

public class GetQuestionnaireResponse
{
    public Questionnaire Questionnaire { get; set; }

    public GetQuestionnaireResponse(Questionnaire questionnaire)
    {
        Questionnaire = questionnaire;
    }
}