using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NaturalnieApp2.Views.Controls
{
    /// <summary>
    /// Interaction logic for BottomButtonBarView.xaml
    /// </summary>
    public partial class BottomButtonBarView : UserControl
    {
        public BottomButtonBarView()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue as BottomButtonBarModel == null)
                throw new InvalidDataException($"Wrong data type. DataContex for the BottomButtonBarView must be of type BottomButtonBarModel");
        }

    }
}
