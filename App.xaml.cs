using Microsoft.Extensions.DependencyInjection;
using NaturalnieApp2.Services;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels;
using NaturalnieApp2.ViewModels.MenuScreens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NaturalnieApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()

            });

            services.AddSingleton<INavigationService>(s => CreateMainScreen(s));

            services.AddSingleton<MainScreenViewModel>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateMainScreen(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<MainScreenViewModel>(serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<MainScreenViewModel>());
        }
    }
}
