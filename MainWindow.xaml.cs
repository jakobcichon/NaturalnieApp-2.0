using NaturalnieApp2.ViewModels;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaturalnieApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double slidePanelOpenValue;
        private readonly double slidePanelCloseValue;

        public MainWindow()
        {
            InitializeComponent();

            slidePanelOpenValue = MainGrid.ColumnDefinitions[0].MaxWidth;
            slidePanelCloseValue = MainGrid.ColumnDefinitions[0].MinWidth;
            var test = new GridSplitter();
        }

        

        public double TargetSlidePanelValue
        {
            get { return (double)GetValue(TargetSlidePanelValueProperty); }
            set { SetValue(TargetSlidePanelValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetSlidePanelValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetSlidePanelValueProperty =
            DependencyProperty.Register("TargetSlidePanelValue", typeof(double), typeof(MainWindow), new PropertyMetadata(40.0d));

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            if (TargetSlidePanelValue != slidePanelCloseValue)
            {
                TargetSlidePanelValue = slidePanelCloseValue;
                return;
            }

            TargetSlidePanelValue = slidePanelOpenValue;
        }
    }
}