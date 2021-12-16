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

            // Create an instance for the main window
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });

            #region Menu bar items
            // Create an isntance of the Menu Bar View Model
            services.AddSingleton(s => new MainWindowViewModel(
                s.GetRequiredService<MenuBarViewModel>(), 
                s.GetRequiredService<InitialScreenViewModel>()));

            services.AddSingleton(s => new MenuBarViewModel());
            #endregion

            #region Screens definition
            //Initial screen
            services.AddSingleton<InitialScreenViewModel>();

            //Inventorization
            services.AddSingleton<ExecuteInventorizationViewModel>();
            #endregion

            #region Screen navigation manager
            services.AddSingleton<NavigationDispatcher>();
            #endregion

            //Build service provider
            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();


            _serviceProvider.GetRequiredService<MenuBarViewModel>().AddMenuBarMainButton(CreateMenuBarMainButtons());


            MainWindow.Show();

            base.OnStartup(e);
        }

        private List<MainButtonViewModel> CreateMenuBarMainButtons()
        {
            return new List<MainButtonViewModel>()
            {
                new MainButtonViewModel("Ekran główny"),
                new MainButtonViewModel("Inwentaryzacja")
            };
        }

    }
}
