using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MV1000.Forms;
using MV1000.Forms.UI.Views;
using MV1000.Forms.ViewModels;

namespace MV1000;

public class App : Application
{
    private IHost? _host;

    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            })
            .Build();

        ServiceProvider = _host.Services;
        AppServices.Current = _host.Services;

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
        mainWindow.Left = 0;
        mainWindow.Top = 0;
        mainWindow.Width = 800;
        mainWindow.Height = 600;
        mainWindow.HistoryText = "History";
        mainWindow.HistoryOffImage = new System.Windows.Media.Imaging.BitmapImage(
            new Uri("pack://application:,,,/MV1000.Support;component/Resources/HISTORY_BACK_OFF.png"));
        mainWindow.HistoryOnImage = new System.Windows.Media.Imaging.BitmapImage(
            new Uri("pack://application:,,,/MV1000.Support;component/Resources/HISTORY_BACK_ON.png"));
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host?.Dispose();
        base.OnExit(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
    }
}
