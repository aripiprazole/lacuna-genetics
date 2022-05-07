using System.Text.Json.Serialization;

namespace LacunaGenetics.Jobs;

public class TaskResponse
{
    public string Code { get; }
    public string? Message { get; }

    [JsonConstructor]
    public TaskResponse(string code, string? message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        var str = $"TaskResponse{{Code: {Code}";

        if (Message != null)
            str += $", Message: {Message}";

        return str + "}";
    }
}