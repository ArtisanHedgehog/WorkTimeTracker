using System.ComponentModel;
using System.Windows.Input;

namespace WorkTimeTracker.ViewModels;
public class TaskViewModel : INotifyPropertyChanged
{
    private string _name;
    private ICommand _copyNameToClipboard;

    public event PropertyChangedEventHandler PropertyChanged;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
    }

    public ICommand CopyNameToClipboard
    {
        get => _copyNameToClipboard
            ?? (_copyNameToClipboard = new Command(
                execute: async () => await CopyTaskNameToClipboard()));
        private set => _copyNameToClipboard = value;
    }

    public async Task CopyTaskNameToClipboard()
    {
        await Clipboard.SetTextAsync(Name);
    }
}
