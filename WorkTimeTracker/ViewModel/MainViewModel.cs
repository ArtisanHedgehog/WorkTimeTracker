using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WorkTimeTracker.Model;

namespace WorkTimeTracker.ViewModel;
public partial class MainViewModel : ObservableObject, IDisposable
{
    private readonly IDispatcherTimer _dispatcherTimer;

    public MainViewModel(IDispatcherTimer dispatcherTimer)
    {
        _tasks = new();
        _dispatcherTimer = dispatcherTimer;
        _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
        _dispatcherTimer.Start();
    }

    public string Title => "Tasks";

    [ObservableProperty]
    private ObservableCollection<WorkTask> _tasks;

    [ObservableProperty]
    private string _text;

    [RelayCommand]
    public void Add()
    {
        if (string.IsNullOrEmpty(Text))
        {
            return;
        }
        Tasks.Add(new(Text));
        Text = string.Empty;
    }

    [RelayCommand]
    public void StartTask(WorkTask task)
    {
        _dispatcherTimer.Tick -= task.AddTime;
        _dispatcherTimer.Tick += task.AddTime;
        task.IsInProcess = true;
    }

    [RelayCommand]
    public void StopTask(WorkTask task)
    {
        _dispatcherTimer.Tick -= task.AddTime;
        task.IsInProcess = false;
    }

    public void Dispose()
    {
        _dispatcherTimer.Stop();
    }
}
