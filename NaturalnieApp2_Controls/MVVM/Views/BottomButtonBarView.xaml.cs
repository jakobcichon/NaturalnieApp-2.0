using NaturalnieApp2_Controls.Views.Controls.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NaturalnieApp2_Controls.Views.Controls
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
