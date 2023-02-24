using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkTimeTracker.Model;

public partial class WorkTask : ObservableObject
{
    public WorkTask(string title)
    {
        _title = title;
        _duration = TimeSpan.Zero;
    }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeSpentString))]
    private TimeSpan _duration;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPaused))]
    public bool _isInProcess = false;

    public bool IsPaused => !IsInProcess;

    public string TimeSpentString => $"{Duration.TotalHours:F0}:{Duration.Seconds:D2}";

    public void AddTime(object sender, EventArgs e)
    {
        if(sender is IDispatcherTimer timer)
        {
            Duration += timer.Interval;
        }
    }
}
