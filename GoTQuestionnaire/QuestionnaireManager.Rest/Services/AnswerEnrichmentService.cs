using QuestionnaireManager.Rest.Model.Response;

namespace QuestionnaireManager.Rest.Services;

public interface IAnswerEnrichmentService
{
    Task<string> EnrichAnswerAsync(string answer);
}

public class AnswerEnrichmentService : IAnswerEnrichmentService
{
    private readonly GoTApiService _goTApiService;

    public AnswerEnrichmentService(GoTApiService goTApiService)
    {
        _goTApiService = goTApiService;
    }

    public async Task<string> EnrichAnswerAsync(string answer)
    {
        var houseId = answer switch
        {
            not null when answer.Contains("Targaryen") => 378,
            not null when answer.Contains("Stark") => 362,
            not null when answer.Contains("Lannister") => 229,
            _ => 0
        };

        if (houseId == 0) return answer;
            
        var house = await _goTApiService.GetHouseByIdAsync(houseId);

        return $"{house.Name}, situated in the region: {house.Region}. The house's words are: {house.Words}";
    }
}