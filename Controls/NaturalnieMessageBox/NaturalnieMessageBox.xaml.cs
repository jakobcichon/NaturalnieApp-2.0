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
using System.Windows.Shapes;

namespace NaturalnieApp2.Controls.NaturalnieMessageBox
{
    /// <summary>
    /// Interaction logic for NaturalnieMessageBox.xaml
    /// </summary>
    public partial class NaturalnieMessageBox : Window
    {
        public NaturalnieMessageBox()
        {
            InitializeComponent();
            
        }


        public new static object Show()
        {
            var test = new NaturalnieMessageBox();
            test.ShowDialog(test as Window);

            return null;
        }
        

        /// <summary>
        /// Method used to hide base Show method
        /// </summary>
        private new void ShowDialog(Window window) 
        { 
            window.Show();
        }
    }
}
