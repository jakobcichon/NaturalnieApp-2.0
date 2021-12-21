using Microsoft.Extensions.DependencyInjection;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Sandbox;
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

            //Build service provider
            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
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

            //Show window
            MainWindow.Show();

            base.OnStartup(e);

            //Create keyboard key handler
            var window = Current.MainWindow;
            var source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
            source.AddHook(WndProc);
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

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_KEYDOWN = 0x100;
            if (msg == WM_KEYDOWN)
            {
                VirtualKey keyValue = (VirtualKey)wParam;
                ;
            }

            return IntPtr.Zero;
        }

    }

}
