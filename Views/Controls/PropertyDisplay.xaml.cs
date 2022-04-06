using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PropertyDisplay.xaml
    /// </summary>
    public partial class PropertyDisplay : UserControl
    {
        public PropertyDisplay()
        {
            InitializeComponent();
            SetContentForVisualPresenter(VisualPresenterType);
        }

        #region Dependency properties
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(name: "HeaderText", typeof(string), 
            typeof(PropertyDisplay), new PropertyMetadata(null));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        public static readonly DependencyProperty VisualPresenterTypeProperty = DependencyProperty.Register(name: "VisualPresenterType", typeof(VisualPresenterTypes),
        typeof(PropertyDisplay), new FrameworkPropertyMetadata(defaultValue: VisualPresenterTypes.Field, 
            new PropertyChangedCallback(VisualPresenterTypeChangeCallback)));

        private static void VisualPresenterTypeChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PropertyDisplay? localSource = source as PropertyDisplay;
            if (localSource == null) return;

            localSource.SetContentForVisualPresenter((VisualPresenterTypes)e.NewValue);
        }

        public VisualPresenterTypes VisualPresenterType
        {
            get => (VisualPresenterTypes)GetValue(VisualPresenterTypeProperty);
            set => SetValue(VisualPresenterTypeProperty, value);

        }
        #endregion


        #region Private methods
        private void SetContentForVisualPresenter(VisualPresenterTypes type)
        {
            if (type == VisualPresenterTypes.Field)
            {
                FrameworkElement textBlock = new TextBlock();
                ContentForVisualPresenter.Content = textBlock.Loa;
            }
        }
        #endregion
    }

    public enum VisualPresenterTypes
    {
        Field,
        List
    }
}
