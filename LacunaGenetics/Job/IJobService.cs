namespace LacunaGenetics.Job;

public interface IJobService
{
    public Task<Job> GetJob();

    public Task RunJob(Job job);
}