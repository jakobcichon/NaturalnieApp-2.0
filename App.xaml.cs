using Microsoft.Extensions.DependencyInjection;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Sandbox;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels;
using NaturalnieApp2.ViewModels.Menu;
using NaturalnieApp2.ViewModels.MenuScreens;
using NaturalnieApp2.ViewModels.MenuScreens.Inventory;
using NaturalnieApp2.ViewModels.SplashScreen;
using NaturalnieApp2.Views.SplashScreen;
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
using NaturalnieApp2.Interfaces.SplashScreen;
using System.Data.Entity;
using NaturalnieApp.Database;
using NaturalnieApp2.Services.Database;
using NaturalnieApp2.ViewModels.MenuScreens.Product;

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

            // Service collection instnace
            IServiceCollection services = new ServiceCollection();

            // Register dependency injection objects
            RegisterDependecyInjectionObjects(services);

            //Build service provider
            _serviceProvider = services.BuildServiceProvider();

        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            // Call base method
            base.OnStartup(e);

            // Get instnace of the splash screen
            ISplashScreen _splashScreenViewModel = _serviceProvider.GetRequiredService<SplashScreenViewModel>();

            // Set Main Window property
            Window splashScreen = _serviceProvider.GetRequiredService<SplashScreenView>();

            // Assign to the Main Window
            MainWindow = splashScreen;

            // Show main screen
            splashScreen.Show();
            
            // Run task to initialize recourses
            await RunSTATTask(() =>
            {
                // Cal helper method, to initialize all recourses
                InitializeObjects(_splashScreenViewModel);

                // Close splash screen and switch to MainWindow
                Dispatcher.Invoke(() =>
                {
                    // Get Main Window
                    Window mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

                    //Show window
                    MainWindow = mainWindow;

                    // Show main window
                    mainWindow.Show();

                    //Close splash screen
                    splashScreen.Close();
                });
            });
        }


        private static Task RunSTATTask(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;

        }

        private void InitializeObjects(ISplashScreen _splashScreen)
        {
            _splashScreen.UpdateText("Konfigurowanie menagera zadań");

            //Initailize event manager
            InitializeEventManager();

            _splashScreen.UpdateText("Tworzenie okna głownego");

            //Initialize main window
            InitializeMainWindow();

            _splashScreen.UpdateText("Tworzenie przycisków menu");

            //Initialize menu bar main buttons
            InitializeMenuBarMainButtons();

            _splashScreen.UpdateText("Konfigurowanie przycisków Menu Produktu");

            //Initialize product sub buttons
            InitializeSubMenuButtons_Product(_serviceProvider.GetRequiredService<MenuBarViewModel>(), _serviceProvider);

            _splashScreen.UpdateText("Konfigurowanie przycisków Inwentaryzacji");

            //Initialize inventory sub buttons
            InitializeSubMenuButtons_Inventory(_serviceProvider.GetRequiredService<MenuBarViewModel>(), _serviceProvider);

            _splashScreen.UpdateText("Konfigurowanie przycisków Sandbox");

            // Initialize SubMenuButtons
            InitializeSubMenuButtons_Sandbox(_serviceProvider.GetRequiredService<MenuBarViewModel>(), _serviceProvider);

            _splashScreen.UpdateText("Konfigurowanie właściwości obiektu 'Wykonaj inwentaryzację'");

            // Initialize Execute Inventory Properties
            InitailizeExecuteInventoryProperties();

            _splashScreen.UpdateText("Konfigurowanie właściwości obiektu 'Pokaż inwentaryzację'");

            // Initialize Show Inventory Properties
            InitializeShowInventoryProperties();

            _splashScreen.UpdateText("Ładowanie zasobów Entity Framework");

            //EntityFramework loading resources
            InitailizeEntityFrameworkRecourses(_serviceProvider);

        }

        /// <summary>
        /// Method uset to initialize ShowInventoryProperties
        /// </summary>
        private void InitializeShowInventoryProperties()
        {
            _serviceProvider.GetRequiredService<ShowProductViewModel>();
        }

        /// <summary>
        /// Method uset to initialize ExecuteInventoryProperties
        /// </summary>
        private void InitailizeExecuteInventoryProperties()
        {
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().ModelProvider =
                _serviceProvider.GetRequiredService<ProductProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().StockProvider =
                _serviceProvider.GetRequiredService<StockProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().InventoryProvider =
                _serviceProvider.GetRequiredService<InventoryProvider>();
            _serviceProvider.GetRequiredService<ExecuteInventoryViewModel>().ScreenDipatcher =
                _serviceProvider.GetRequiredService<NavigationDispatcher>();
        }

        /// <summary>
        /// Method used to register required dependecy injection objects
        /// </summary>
        /// <param name="services">Instnace of the IServiceCollection class</param>
        private static void RegisterDependecyInjectionObjects(IServiceCollection services)
        {
            // Singleto for for the Main Window View Model
            services.AddSingleton(s => new MainWindowViewModel(
                s.GetRequiredService<MenuBarViewModel>(),
                s.GetRequiredService<InitialScreenViewModel>()));

            // Singleton for the MainWindow
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });

            // Singleton for the Splash Screen View Model
            services.AddSingleton(s => new SplashScreenViewModel());

            // Singleton for the Splash Screen View
            services.AddSingleton(s => new SplashScreenView()
            {
                DataContext = s.GetRequiredService<SplashScreenViewModel>()
            });

            // Singleton for the Menu Bar View Model
            services.AddSingleton(s => new MenuBarViewModel(s.GetRequiredService<InitialScreenViewModel>(),
                                    s.GetRequiredService<NavigationDispatcher>()));

            //Singleton for the Initial Screen View Model
            services.AddSingleton<InitialScreenViewModel>();

            //Singleton for the ShowProductViewModel
            services.AddSingleton<ShowProductViewModel>(s => new ShowProductViewModel(s.GetRequiredService<ProductProvider>(), 
                s.GetRequiredService<TaxProvider>()));

            //Singleton for the ExecuteInventoryViewModel
            services.AddSingleton<ExecuteInventoryViewModel>();

            //Singleton for the ShowInventoryViewModel
            services.AddSingleton<ShowInventoryViewModel>();

            //Singleton for the SandboxViewModel
            services.AddSingleton<SandboxViewModel>(s => new SandboxViewModel(s.GetRequiredService<TaxProvider>(), s.GetRequiredService<ProductProvider>()));

            //Singleton for the NavigationDispatcher
            services.AddSingleton(s => new NavigationDispatcher());


            //!!!!!!!!!!!!!!!!!!!To be replaced with singleton class!!!!!!!!!!!!!!!!1
            /*            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                        "uid = naturalnie_admin; password = Tojestnajlepszaaplikacja2.0; Connection Timeout = 10", "naturalnieapp.mysql.database.azure.com");*/
            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                        "uid = admin; password = admin; Connection Timeout = 10", "desktop-l2l4v68");
            /*            string connectionString = string.Format("server = {0}; port = 3306; database = shop;" +
                        "uid = admin; password = admin; Connection Timeout = 10", "localhost");*/

            //Transient for the ShopContext
            services.AddTransient<ShopContext>(s => new ShopContext(connectionString));

            //Transient for the DatabaseCommon
            services.AddTransient<DatabaseCommon>(s => new DatabaseCommon(s.GetRequiredService<ShopContext>()));

            //Transient for the ProductProvider
            services.AddTransient<ProductProvider>(s => new ProductProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the StockProvider
            services.AddTransient<StockProvider>(s => new StockProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the TaxProvider
            services.AddTransient<TaxProvider>(s => new TaxProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the ManufacturerProvider
            services.AddTransient<ManufacturerProvider>(s => new ManufacturerProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the SupplierProvider
            services.AddTransient<SupplierProvider>(s => new SupplierProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the InventoryProvider
            services.AddTransient<InventoryProvider>(s => new InventoryProvider(s.GetRequiredService<ShopContext>()));

            //Transient for the InventoryHistoryProvider
            services.AddTransient<InventoryHistoryProvider>(s => new InventoryHistoryProvider(s.GetRequiredService<ShopContext>()));
        }

        /// <summary>
        /// Method uset to initialize MenuBarMainButtons
        /// </summary>
        private void InitializeMenuBarMainButtons()
        {
            _serviceProvider.GetRequiredService<MenuBarViewModel>().AddMenuBarMainButton(CreateMenuBarMainButtons());
        }
         
        /// <summary>
        /// Method uset to initialize EventManager
        /// </summary>
        private void InitializeEventManager()
        {
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent,
                new KeyEventHandler(_serviceProvider.GetRequiredService<MainWindowViewModel>().OnKeyDown), true);
        }

        /// <summary>
        /// Method uset to initialize Main Window
        /// </summary>
        private void InitializeMainWindow()
        {
            //Add main view to the view dispatcher
            _serviceProvider.GetRequiredService<NavigationDispatcher>().
                AddHostScreen(_serviceProvider.GetRequiredService<MainWindowViewModel>());
        }

        /// <summary>
        /// Method uset to create Menu bar main buttons
        /// </summary>
        private List<MainButtonViewModel> CreateMenuBarMainButtons()
        {
            return new List<MainButtonViewModel>()
            {
                new MainButtonViewModel(MenuButtonNames.ProductButton),
                new MainButtonViewModel(MenuButtonNames.InventoryButton),
                new MainButtonViewModel(MenuButtonNames.Sandbox)
            };
        }

        /// <summary>
        /// Method used to create Product sub menu
        /// </summary>
        /// <param name="menuBar">Instnace of the MenuBarViewModel</param>
        /// <param name="service">Instance of the IServiceProvider </param>
        private void InitializeSubMenuButtons_Product(MenuBarViewModel menuBar, IServiceProvider service)
        {
            menuBar.MenuBarViews.Where(m => m.Name == MenuButtonNames.ProductButton).
                FirstOrDefault()?.AddSubButton(new List<ISubMenuButton>()
            {
                new SubButtonViewModel("Pokaż produkt",
                service.GetRequiredService<ShowProductViewModel>(),
                service.GetRequiredService<NavigationDispatcher>()
                )
            });
        }

        /// <summary>
        /// Method used to create Inventory sub menu
        /// </summary>
        /// <param name="menuBar">Instnace of the MenuBarViewModel</param>
        /// <param name="service">Instance of the IServiceProvider </param>
        private void InitializeSubMenuButtons_Inventory(MenuBarViewModel menuBar, IServiceProvider service)
        {
            menuBar.MenuBarViews.Where(m => m.Name == MenuButtonNames.InventoryButton).
                FirstOrDefault()?.AddSubButton(new List<ISubMenuButton>()
            {
                new SubButtonViewModel("Wykonaj inwentaryzację",
                service.GetRequiredService<ExecuteInventoryViewModel>(),
                service.GetRequiredService<NavigationDispatcher>()
                )
            });

            menuBar.MenuBarViews.Where(m => m.Name == MenuButtonNames.InventoryButton).
                FirstOrDefault()?.AddSubButton(new List<ISubMenuButton>()
            {
                new SubButtonViewModel("Pokaż inwentaryzację",
                service.GetRequiredService<ShowInventoryViewModel>(),
                service.GetRequiredService<NavigationDispatcher>()
                )
            });
        }

        /// <summary>
        /// Method used to load recouser of the EnitytFramwork
        /// </summary>
        /// <param name="menuBar">Instnace of the MenuBarViewModel</param>
        /// <param name="service">Instance of the IServiceProvider </param>
        private void InitailizeEntityFrameworkRecourses(IServiceProvider service)
        {
            service.GetRequiredService<DatabaseCommon>().CheckDatabaseConnection();
        }

        /// <summary>
        /// Method uset to initialize Sandbox buttons
        /// </summary>
        private void InitializeSubMenuButtons_Sandbox(MenuBarViewModel menuBar, IServiceProvider service)
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

            public static string ProductButton = "Produkt";
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

           // ProductProvider productProvider = new ProductProvider(ShopContext);
            //List<object> test = productProvider.GetAllModelData();
            ;
        }
    }

}
