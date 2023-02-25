using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult FireAndForgot()
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire And Forgot"));
            return Ok($"************************* [ {jobId} Fire And Forgot ] *************************");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Delay()
        {
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delay Task"), TimeSpan.FromSeconds(10));
            return Ok($"************************* [ {jobId} Delay Task ] *************************");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Recurring()
        {
            RecurringJob.AddOrUpdate("MyreCurringJob", () => Console.WriteLine("Recurring Task"), Cron.Minutely);
            return Ok($"************************* [ Recurring Task ] *************************");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Continuations()
        {
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Wait for Continuous Task"), TimeSpan.FromSeconds(10));
            var secondJob = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuous Task!"));

            return Ok($"************************* [ {jobId} : {secondJob} TestContinuous Task ] *************************");
        }
    }
}
