using Microsoft.Extensions.DependencyInjection;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels;
using NaturalnieApp2.ViewModels.Menu;
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

            services.AddSingleton<MainButtonViewModel>();

            services.AddSingleton<NavigationDispatcher>();
            

            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider.GetRequiredService<MainButtonViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
