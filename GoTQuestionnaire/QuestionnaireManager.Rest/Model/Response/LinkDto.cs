namespace QuestionnaireManager.Rest.Model.Response;

public class LinkDto
{
    public string? Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }
}