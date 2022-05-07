namespace LacunaGenetics.Job;

public class EncodeStrandRequest
{
    public string StrandEncoded { get; }

    public EncodeStrandRequest(string strandEncoded)
    {
        StrandEncoded = strandEncoded;
    }
}