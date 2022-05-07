using LacunaGenetics.Auth;
using LacunaGenetics.Jobs;

namespace LacunaGenetics;

public static class Program
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://gene.lacuna.cc/") };

    public static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            await SendUsage();
            return;
        }

        var command = args[0];

        switch (command)
        {
            case "register":
                if (args.Length < 4)
                {
                    await Console.Error.WriteLineAsync("Usage: gene register <username> <email> <password>");
                    return;
                }

                await RunRegister(args[1], args[2], args[3]);
                break;

            case "run":
                if (args.Length < 3)
                {
                    await Console.Error.WriteLineAsync("Usage: gene run <username> <password>");
                    return;
                }

                await RunJob(args[1], args[2]);
                break;

            default:
                await SendUsage();
                break;
        }
    }

    private static async Task SendUsage()
    {
        await Console.Error.WriteLineAsync("Usage: gene <args>");
        await Console.Error.WriteLineAsync("  gene register <username> <email> <password>");
        await Console.Error.WriteLineAsync("  gene run <username> <password>");
    }

    private static async Task RunRegister(string username, string email, string password)
    {
        var authService = new AuthServiceImpl(Client);
        await authService.Register(new RegisterRequest(username, email, password));

        Console.WriteLine($"Successfully registered user: {username}");
    }

    private static async Task RunJob(string username, string password)
    {
        var authService = new AuthServiceImpl(Client);
        var jobService = new JobServiceImpl(Client);

        await authService.Login(new LoginRequest(username, password));
        Console.WriteLine($"Successfully authenticated as {username}");

        var job = await jobService.GetJob();

        Console.WriteLine($"Running job: {job.Id}");
        Console.WriteLine($"  Job type: {job.Type}");

        await jobService.RunJob(job);

        Console.WriteLine("  Job ran successful");
    }
}