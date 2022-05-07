using System.Text.Json.Serialization;

namespace LacunaGenetics.Jobs;

public class JobResponse
{
    public Job Job { get; }
    public string Code { get; }
    public string? Message { get; }

    [JsonConstructor]
    public JobResponse(Job job, string code, string? message)
    {
        Job = job;
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        var str = $"JobResponse{{Job: {Job}, Code: {Code}";

        if (Message != null)
            str += $", Message: {Message}";

        return str + "}";
    }
}