using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaturalnieApp2.Views.Menu
{
    /// <summary>
    /// Interaction logic for MenuBarItem.xaml
    /// </summary>
    public partial class MainButtonView : UserControl
    {
        private static Button lastSelectedButton;

        public MainButtonView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? localButton = sender as Button;
            if (localButton == null) return;

            if (lastSelectedButton != null)
            {
                lastSelectedButton.Style = FindResource("UnselectedButtonStyle") as Style;
            }

            lastSelectedButton = localButton;
            lastSelectedButton.Style = FindResource("SelectedButtonStyle") as Style;

        }
    }
}
