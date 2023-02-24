using Microsoft.Extensions.Logging;
using WorkTimeTracker.ViewModel;

namespace WorkTimeTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("open-iconic.ttf", "Icons");
            });

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<IDispatcherTimer>(sp =>
			DispatcherProvider.Current.GetForCurrentThread().CreateTimer());


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
