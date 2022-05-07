namespace LacunaGenetics.Job;

public class CheckGeneRequest
{
    public bool IsActivated { get; }

    public CheckGeneRequest(bool isActivated)
    {
        IsActivated = isActivated;
    }
}