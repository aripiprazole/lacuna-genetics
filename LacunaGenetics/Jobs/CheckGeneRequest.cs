namespace LacunaGenetics.Jobs;

public class CheckGeneRequest
{
    public bool IsActivated { get; }

    public CheckGeneRequest(bool isActivated)
    {
        IsActivated = isActivated;
    }
}