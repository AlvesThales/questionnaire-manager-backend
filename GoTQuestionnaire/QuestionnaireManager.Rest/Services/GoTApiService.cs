namespace QuestionnaireManager.Rest.Services;

public class GoTApiService
{
    private readonly HttpClient _httpClient;

    public GoTApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://anapioficeandfire.com");
    }

    public async Task<House> GetHouseByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/houses/{id}");
        response.EnsureSuccessStatusCode();

        var house = await response.Content.ReadFromJsonAsync<House>();
        return house;
    }
}
