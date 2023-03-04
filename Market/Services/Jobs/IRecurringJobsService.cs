namespace Market.Services.Jobs;

public interface IRecurringJobsService
{
    /// <summary>
    /// Create a set of recurring jobs with their own cron timers
    /// which should be noted in the appSettings.
    /// </summary>
    void CreateRecurringJobs();
}