using ToDoMauiClient.DataServices;
using ToDoMauiClient.Pages;

namespace ToDoMauiClient;

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
			});

		//HttpClient we need to manage it 
		//builder.Services.AddSingleton<IRestDataService, RestDataService>();

		//Httpclient <-- is managed for use by Microsoft.Extensions.Http
		builder.Services.AddHttpClient<IRestDataService, RestDataService>();



    builder.Services.AddSingleton<MainPage>(); //Add the page for dependency injection 
		builder.Services.AddTransient<ManageToDoPage>();

		return builder.Build();
	}
}
