namespace QuestionnaireManager.Rest.Services;

public record House
{
    public string Url { get; init; }
    public string Name { get; init; }
    public string Region { get; init; }
    public string CoatOfArms { get; init; }
    public string Words { get; init; }
    public string[] Titles { get; init; }
    public string[] Seats { get; init; }
    public string CurrentLord { get; init; }
    public string Heir { get; init; }
    public string Overlord { get; init; }
    public string Founded { get; init; }
    public string Founder { get; init; }
    public string DiedOut { get; init; }
    public string[] AncestralWeapons { get; init; }
    public string[] CadetBranches { get; init; }
    public string[] SwornMembers { get; init; }
}