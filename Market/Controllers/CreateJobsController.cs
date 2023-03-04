using Market.Services.Jobs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Market.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreateJobsController : ControllerBase
    {
        private readonly ILogger<CreateJobsController> logger;
        private readonly IRecurringJobsService recurringJobsService;

        public CreateJobsController(
            ILogger<CreateJobsController> logger,
            IRecurringJobsService recurringJobsService)
        {
            this.logger = logger;
            this.recurringJobsService = recurringJobsService;
        }

        [HttpGet(Name = "SetJobs")]
        public HttpStatusCode Get()
        {
            try
            {
                recurringJobsService.CreateRecurringJobs();
            }
            catch (Exception e)
            {
                logger.LogError($"An error ocurred creating recurring jobs. Error: {e}");
            }

            return HttpStatusCode.OK;
        }
    }
}
