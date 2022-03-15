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

namespace NaturalnieApp2.Sandbox
{
    /// <summary>
    /// Interaction logic for SandboxViewxaml.xaml
    /// </summary>
    public partial class SandboxView : UserControl
    {
        public SandboxView()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SandboxViewModel).OnButtonClick();
        }
    }
}
