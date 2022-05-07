using LacunaGenetics.Auth;
using LacunaGenetics.Jobs;

namespace LacunaGenetics;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var client = new HttpClient { BaseAddress = new Uri("https://gene.lacuna.cc/") };
        var authService = new AuthServiceImpl(client);
        var jobService = new JobServiceImpl(client);

        await authService.Login(new LoginRequest("gabrielleeg1", "----"));
        var job = await jobService.GetJob();
        
        Console.WriteLine($"Running job: {job.Id}");
        Console.WriteLine($"  Job type: {job.Type}");

        await jobService.RunJob(job);
        
        Console.WriteLine("  Job ran successful");
    }
}