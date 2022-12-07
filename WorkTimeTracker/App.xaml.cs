namespace WorkTimeTracker;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
		var window = base.CreateWindow(activationState);

		const int newWidth = 400;
		const int newHeight = 600;

		var disp = DeviceDisplay.MainDisplayInfo;

        window.X = disp.Width / disp.Density - newWidth;
		window.Y = disp.Height / disp.Density - newHeight;
		

		window.Width = newWidth;
		window.Height = newHeight;

		return window;
    }
}
