namespace WorkTimeTracker.Models;

public class WorkTask
{
    public string Title { get; set; }
    public TimeSpan ElapsedTime { get; private set; } = TimeSpan.Zero;

    internal void AddTimeSpan(TimeSpan deltaTime)
    {
        ElapsedTime += deltaTime;
    }
}
