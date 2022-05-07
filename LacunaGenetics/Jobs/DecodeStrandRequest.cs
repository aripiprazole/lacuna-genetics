namespace LacunaGenetics.Jobs;

public class DecodeStrandRequest
{
    public string Strand { get; }

    public DecodeStrandRequest(string strand)
    {
        Strand = strand;
    }
}