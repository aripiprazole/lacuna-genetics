namespace LacunaGenetics.Jobs;

public interface IJobService
{
    public Task<Job> GetJob();

    public Task RunJob(Job job);
}