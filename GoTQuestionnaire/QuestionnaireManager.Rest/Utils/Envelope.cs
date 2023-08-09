namespace QuestionnaireManager.Rest.Utils;

public class Envelope<T>
{
    public Envelope(T result, string? errorMessage, IEnumerable<string> errorCodes)
    {
        Result = result;
        ErrorMessage = errorMessage;
        ErrorCodes = errorCodes;
        TimeGenerated = DateTime.UtcNow;
    }
        
    public T Result { get; set; }
    public string? ErrorMessage { get; } 
    
    public IEnumerable<string> ErrorCodes { get; }
    public DateTime TimeGenerated { get; }
}
    
public sealed class Envelope : Envelope<string>
{
    public Envelope(string result, string errorMessage, IEnumerable<string> errorCodes) :base(result,errorMessage,errorCodes)
    {
                
    }
    private Envelope(string? errorMessage)
        : base(null!, errorMessage, Enumerable.Empty<string>())
    {}

    public static Envelope<T> Ok<T>(T result) => new(result, null, Enumerable.Empty<string>());
        
    public static Envelope Ok() => new (null);

    public static Envelope Error(string? errorMessage) => new (errorMessage);
}