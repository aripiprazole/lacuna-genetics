namespace LacunaGenetics.Jobs;

public class Job
{
    public string Id { get; }
    public string Type { get; }
    public string? Strand { get; }
    public string? StrandEncoded { get; }
    public string? GeneEncoded { get; }

    public Job(string id, string type, string? strand, string? strandEncoded, string? geneEncoded)
    {
        Id = id;
        Type = type;
        Strand = strand;
        StrandEncoded = strandEncoded;
        GeneEncoded = geneEncoded;
    }

    public override string ToString()
    {
        var str = $"Job{{Id: {Id}, Type: {Type}";

        if (Strand != null)
            str += $", Strand: {Strand}";

        if (StrandEncoded != null)
            str += $", StrandEncoded: {StrandEncoded}";

        if (GeneEncoded != null)
            str += $", GeneEncoded: {GeneEncoded}";

        return str + "}";
    }

    public static bool CheckGene(string gene, string strand)
    {
        for (var i = 0; i < gene.Length; i++)
        for (var j = i; j < gene.Length; j++)
        {
            if (j - i < (gene.Length / 2)) continue;
            if (j == i) continue;

            if (strand.Contains(gene.Substring(i, j - i)))
                return true;
        }

        return false;
    }

    public static string EncodeStrand(string strand)
    {
        var bytes = new List<byte>();

        for (var j = 0; j < strand.Length; j += 4)
        {
            var codes = EncodeCode(strand[j]) << 2 * 3;
            codes |= EncodeCode(strand[j + 1]) << 2 * 2;
            codes |= EncodeCode(strand[j + 2]) << 2;
            codes |= EncodeCode(strand[j + 3]);
            bytes.Add((byte)codes);
        }

        return Convert.ToBase64String(bytes.ToArray());
    }

    public static string DecodeStrand(string strandEncoded)
    {
        var str = "";

        foreach (var b in Convert.FromBase64String(strandEncoded))
            for (var i = 3; i >= 0; i--)
                str += DecodeCode(0b11 & (b >> 2 * i));

        return str;
    }

    private static byte EncodeCode(char code) => code switch
    {
        'A' => 0b00,
        'T' => 0b11,
        'C' => 0b01,
        'G' => 0b10,
        _ => throw new Exception($"Could not encode code {code}")
    };

    private static char DecodeCode(int x) => x switch
    {
        0b00 => 'A',
        0b11 => 'T',
        0b01 => 'C',
        0b10 => 'G',
        _ => throw new Exception("Could not decode code: " + Convert.ToString(x, 2).PadLeft(8, '0'))
    };
}