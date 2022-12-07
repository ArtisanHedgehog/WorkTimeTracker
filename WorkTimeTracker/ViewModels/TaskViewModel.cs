using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.ViewModels;
public class TaskViewModel : INotifyPropertyChanged
{
    private ICommand _copyTitleToClipboardCommand;
    private ICommand _copyElapsedTimeToClipboardCommand;
    private ICommand _startTimerCommand;
    private ICommand _stopTimerCommand;
    private WorkTask _workTask;
    private DateTime _timerLastTick;
    private IDispatcherTimer _timer;

    public TaskViewModel()
    {
        _copyTitleToClipboardCommand = new Command(async () => await CopyTitleToClipboard());
        _copyElapsedTimeToClipboardCommand = new Command(async () => await CopyElapsedTimeToClipboard());
        _startTimerCommand = new Command(StartTimer);
        _stopTimerCommand = new Command(StopTimer);
        _workTask = new();
        _timer = DispatcherProvider.Current.GetForCurrentThread().CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += TimerTick;
    }

    private void TimerTick(object sender, EventArgs e)
    {
        var deltaTime = DateTime.Now - _timerLastTick;
        _timerLastTick = DateTime.Now;
        if(deltaTime > TimeSpan.Zero)
        {
            _workTask.AddTimeSpan(deltaTime);
        }
        PropertyChanged?.Invoke(null, new(nameof(ElapsedTime)));
    }

    public int MyProperty { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public string Title
    {
        get => _workTask.Title;
        set
        {
            _workTask.Title = value;
            PropertyChanged?.Invoke(null, new(nameof(Title)));
        }
    }

    public string ElapsedTime => $"{_workTask.ElapsedTime.TotalHours:N0}:{_workTask.ElapsedTime.Minutes:D2}:{_workTask.ElapsedTime.Seconds:D2}";
    public string ElapsedTimeToClipboard => $"{_workTask.ElapsedTime.TotalHours:N0}ч {_workTask.ElapsedTime.Minutes}м";
    public ICommand CopyTitleToClipboardCommand => _copyTitleToClipboardCommand;
    public ICommand CopyElapsedTimeToClipboardCommand => _copyElapsedTimeToClipboardCommand;
    public ICommand StartTimerCommand => _startTimerCommand;
    public ICommand StopTimerCommand => _stopTimerCommand;


    public void StartTimer()
    {
        _timerLastTick = DateTime.Now;
        _timer.Start();
    }

    public void StopTimer()
    {
        _timer.Stop();
        var deltaTime = DateTime.Now - _timerLastTick;
        _timerLastTick = DateTime.Now;
        if (deltaTime > TimeSpan.Zero)
        {
            _workTask.AddTimeSpan(deltaTime);
        }
        PropertyChanged?.Invoke(null, new(nameof(ElapsedTime)));
    }

    public async Task CopyTitleToClipboard()
    {
        await Clipboard.SetTextAsync(Title);
    }

    public async Task CopyElapsedTimeToClipboard()
    {
        await Clipboard.SetTextAsync(ElapsedTimeToClipboard);
    }
}
