using Microsoft.Extensions.DependencyInjection;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Sandbox;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels;
using NaturalnieApp2.ViewModels.Menu;
using NaturalnieApp2.ViewModels.MenuScreens;
using NaturalnieApp2.ViewModels.MenuScreens.Inventory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Windows.System;
using System.Data.Entity.Core.EntityClient;
using NaturalnieApp2.Models;
using System.IO;
using NaturalnieApp2.Attributes;
using NaturalnieApp2.Views.Controls.Models;

namespace NaturalnieApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public void OnKeyPressed(KeyEventArgs args)
        {
            ;
        }

        public App()
        {

            //!!!!!!!!!!!!!!!!!!!To be replaced with singleton class!!!!!!!!!!!!!!!!1
            /*            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                        "uid = naturalnie_admin; password = Tojestnajlepszaaplikacja2.0; Connection Timeout = 10", "naturalnieapp.mysql.database.azure.com");*/
            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                        "uid = admin; password = admin; Connection Timeout = 10", "desktop-l2l4v68");
/*            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
            "uid = admin; password = admin; Connection Timeout = 10", "localhost");*/


            IServiceCollection services = new ServiceCollection();

            // Create an instance for the main window
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });

            #region Menu bar items
            // Create an instance of the Menu Bar View Model
            services.AddSingleton(s => new MainWindowViewModel(
                s.GetRequiredService<MenuBarViewModel>(), 
                s.GetRequiredService<InitialScreenViewModel>()));

            services.AddSingleton(s => new MenuBarViewModel(s.GetRequiredService<InitialScreenViewModel>(),
                                    s.GetRequiredService<NavigationDispatcher>()));

            #endregion

            #region Screens definition
            //Initial screen
            services.AddSingleton<InitialScreenViewModel>();

            //Inventory
            services.AddSingleton<ExecuteInventoryViewModel>();
            services.AddSingleton<SandboxViewModel>();
            #endregion

            #region Screen navigation manager
            services.AddSingleton(s => new NavigationDispatcher());
            #endregion

            #region Database
            services.AddTransient<ProductProvider>(s => new ProductProvider(connectionString));
            services.AddTransient<StockProvider>(s => new StockProvider(connectionString));
            services.AddTransient<InventoryProvider>(s => new InventoryProvider(connectionString));
            services.AddTransient<InventoryHistoryProvider>(s => new InventoryHistoryProvider(connectionString));

            #endregion

            #region Controls Data Models

            #endregion

            //Build service provider
            _serviceProvider = services.BuildServiceProvider();

        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent, 
                new KeyEventHandler(_serviceProvider.GetRequiredService<MainWindowViewModel>().OnKeyDown), true);

            //Create main window object
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            //Add main view to the view dispatcher
            _serviceProvider.GetRequiredService<NavigationDispatcher>().
                AddHostScreen(_serviceProvider.GetRequiredService<MainWindowViewModel>());

            //Create menu bar main buttons
            _serviceProvider.GetRequiredService<MenuBarViewModel>().AddMenuBarMainButton(CreateMenuBarMainButtons());

            //Add inventory sub buttons
            CreateSubMenuButtons_Inventory(_serviceProvider.GetRequiredService<MenuBarViewModel>(), _serviceProvider);

            CreateSubMenuButtons_Sandbox(_serviceProvider.GetRequiredService<MenuBarViewModel>(), _serviceProvider);

            #region Execute inventory
/*            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().ModelProvider =
                _serviceProvider.GetRequiredService<ProductProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().StockProvider =
                _serviceProvider.GetRequiredService<StockProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().InventoryProvider =
                _serviceProvider.GetRequiredService<InventoryProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().ScreenDipatcher =
                _serviceProvider.GetRequiredService<NavigationDispatcher>();*/

            #endregion

            //Show window
            MainWindow.Show();

            base.OnStartup(e);

            //Sandbox();
        }

        private List<MainButtonViewModel> CreateMenuBarMainButtons()
        {
            return new List<MainButtonViewModel>()
            {
                new MainButtonViewModel(MenuButtonNames.InventoryButton),
                new MainButtonViewModel(MenuButtonNames.Sandbox)
            };
        }

        /// <summary>
        /// Method used to create Inventory sub menu
        /// </summary>
        /// <param name="menuBar"></param>
        /// <param name="service"></param>
        private void CreateSubMenuButtons_Inventory(MenuBarViewModel menuBar, IServiceProvider service)
        {
            menuBar.MenuBarViews.Where(m => m.Name == MenuButtonNames.InventoryButton).
                FirstOrDefault()?.AddSubButton(new List<ISubMenuButton>()
            {
                new SubButtonViewModel("Wykonaj inwentaryację",
                service.GetRequiredService<ExecuteInventoryViewModel>(),
                service.GetRequiredService<NavigationDispatcher>()
                )
            });
        }

        private void CreateSubMenuButtons_Sandbox(MenuBarViewModel menuBar, IServiceProvider service)
        {
            menuBar.MenuBarViews.Where(m => m.Name == MenuButtonNames.Sandbox).
                FirstOrDefault()?.AddSubButton(new List<ISubMenuButton>()
            {
                new SubButtonViewModel("Piaskownica",
                service.GetRequiredService<SandboxViewModel>(),
                service.GetRequiredService<NavigationDispatcher>()
                )
            });
        }

        public static class MenuButtonNames
        {
            public static string MainMenuButton = "Ekran główny";
            public static string InventoryButton = "Inwentaryzacja";

            public static string Sandbox = "Piaskownica";
        }


        public void Sandbox()
        {
            string directory = Environment.CurrentDirectory;
            DirectoryInfo mainDirectory = null;

            while (true) 
            {
                DirectoryInfo _directory = Directory.GetParent(directory);
                if (_directory == null) throw new DirectoryNotFoundException();


                if (_directory.Name == "NaturalnieApp-2.0" )
                {
                    mainDirectory = _directory;
                    break;
                }

                directory = _directory.FullName;
            }

            string fullPath = System.IO.Path.Combine(mainDirectory.FullName, @"AzureDB\DigiCertGlobalRootCA.crt.pem");

            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                    "uid = naturalnie_admin; password = Tojestnajlepszaaplikacja2.0; Connection Timeout = 10", "naturalnieapp.mysql.database.azure.com");

            ProductProvider productProvider = new ProductProvider(connectionString);
            //List<object> test = productProvider.GetAllModelData();
            ;
        }
    }

}
