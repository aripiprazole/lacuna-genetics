using System.Net.Http.Json;
using System.Text.Json;

namespace LacunaGenetics.Jobs;

public class JobServiceImpl : IJobService
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public JobServiceImpl(HttpClient client)
    {
        _client = client;
    }

    public async Task<Job> GetJob()
    {
        var response = await _client.GetAsync("/api/dna/jobs");
        var content = await response.Content.ReadFromJsonAsync<JobResponse>(SerializerOptions);
        if (content == null)
            throw new NullReferenceException("Job response is null");

        if (content.Message != null)
            throw new Exception($"Could not get job: {content.Message}");

        return content.Job;
    }

    public async Task RunJob(Job job)
    {
        switch (job.Type)
        {
            case "CheckGene":
                await RunCheckGeneTask(job);
                break;

            case "EncodeStrand":
                await RunEncodeStrandTask(job);
                break;

            case "DecodeStrand":
                await RunDecodeStrandTask(job);
                break;
        }
    }

    private async Task RunCheckGeneTask(Job job)
    {
        var isActivated = Job.CheckGene(job.GeneEncoded!, Job.DecodeStrand(job.StrandEncoded!));
        
        var response = await _client.PostAsJsonAsync($"/api/dna/jobs/{job.Id}/gene",
            new CheckGeneRequest(isActivated),
            SerializerOptions);

        var content = await response.Content.ReadFromJsonAsync<TaskResponse>();

        if (content == null)
            throw new NullReferenceException("Task response is null");

        if (content.Message != null)
            throw new Exception($"Could not check gene: {content.Message}");
    }
    
    private async Task RunDecodeStrandTask(Job job)
    {
        var strandEncoded = Job.DecodeStrand(job.StrandEncoded!);

        var response = await _client.PostAsJsonAsync($"/api/dna/jobs/{job.Id}/decode",
            new DecodeStrandRequest(strandEncoded),
            SerializerOptions);

        var content = await response.Content.ReadFromJsonAsync<TaskResponse>();

        if (content == null)
            throw new NullReferenceException("Task response is null");

        if (content.Message != null)
            throw new Exception($"Could not decode strand: {content.Message}");
    }

    private async Task RunEncodeStrandTask(Job job)
    {
        var strandEncoded = Job.EncodeStrand(job.Strand!);

        var response = await _client.PostAsJsonAsync($"/api/dna/jobs/{job.Id}/encode",
            new EncodeStrandRequest(strandEncoded),
            SerializerOptions);

        var content = await response.Content.ReadFromJsonAsync<TaskResponse>();

        if (content == null)
            throw new NullReferenceException("Task response is null");

        if (content.Message != null)
            throw new Exception($"Could not encode strand: {content.Message}");
    }
}