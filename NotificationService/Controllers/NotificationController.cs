using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        /// <summary>
        /// Enqueue job and execute as soon as possible
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("fire-and-forget")]
        public IActionResult FireAndForget()
        {
            string jobId = BackgroundJob.Enqueue(
                () => Console.WriteLine("Fire and forget task: execution"));
            return Ok($"Job id: {jobId}");
        }

        /// <summary>
        /// Wait delay then like FireAndForget
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("delayed")]
        public IActionResult Delayed()
        {
            TimeSpan jobDelay = TimeSpan.FromSeconds(10);
            string jobId = BackgroundJob.Schedule(
                () => Console.WriteLine("Delayed task: execution"),
                jobDelay);
            return Ok($"Job id: {jobId}");
        }

        /// <summary>
        /// Wait cronExpression then like FireAndForget
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("recurring")]
        public IActionResult Recurring()
        {
            RecurringJob.AddOrUpdate(
                () => Console.WriteLine("Recurring task: execution"),
                Cron.Minutely);
            return Ok();
        }

        /// <summary>
        /// Wait Parent task execution then execute Continuation task like FireAndForget
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("continuation")]
        public IActionResult Continuation()
        {
            string jobId = BackgroundJob.Enqueue(
                () => Console.WriteLine("Parent task for Continuation task: execution"));

            BackgroundJob.ContinueJobWith(
                jobId,
                () => Console.WriteLine("Continuation task: execution"));
            return Ok($"Job id: {jobId}");
        }
    }
}
