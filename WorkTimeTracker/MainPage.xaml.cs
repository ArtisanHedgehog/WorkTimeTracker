using WorkTimeTracker.ViewModel;

namespace WorkTimeTracker;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
