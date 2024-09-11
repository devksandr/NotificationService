# Notification service
## About
To notify uses `Hangfire` framework for `ASP.NET Core`

## Description
For notifications, `lambda` is usually used:
```csharp
() => { // logic }
```
This project consider such notify methods as:
- `Fire and forget` - when job is created it would be enqueued and executed as soon as possible. It is executed once.
  ```csharp
  string jobId = BackgroundJob.Enqueue(lambda);
  ```
- `Delayed` - when job is created it would wait `delay` then enqueued and executed as soon as possible. It is executed once.
  ```csharp
  string jobId = BackgroundJob.Schedule(lambda, delay);
  ```
- `Recurring` - when job is created it would wait when `cronExpression` is `true` then enqueued and executed as soon as possible. It is executed every time `cronExpression` is `true`.
  ```csharp
  RecurringJob.AddOrUpdate(lambda, cronExpression);
  ```
- `Continuation` - when jobs are created `parent job` would be enqueued and executed as soon as possible. Only then `continuation job` would be enqueued and executed as soon as possible.
  ```csharp
  string parentJobId = BackgroundJob.Enqueue(parentLambda);        // Parent task
  BackgroundJob.ContinueJobWith(parentJobId, continuationLambda);  // Continuation task
  ```
