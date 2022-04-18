using NaturalnieApp2.Attributes;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Views.Controls.Models;
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

        private bool Test { get; set; }
        public Binding PropertyValue
        {
            get { return (Binding)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }

        // Property value
        public static readonly DependencyProperty PropertyValueProperty =
            DependencyProperty.Register("PropertyValue", typeof(Binding), typeof(PropertyDisplay), new PropertyMetadata(null, 
                new PropertyChangedCallback(PropertyValueChangeCallback)));

        private static void PropertyValueChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PropertyDisplay? localSource = source as PropertyDisplay;

            if (localSource == null) return;

            if (localSource.PropertyValue != null)
            {
                ValidationRule? validationRule = null;
                string? propertyName = localSource.PropertyValue.Path?.Path.ToString();
                if (propertyName != null)
                {
                    //Get validation class
                    PropertyDescriptor? property = DisplayModelAttributesServices.GetPropertyByName(localSource.PropertyValue.Source.GetType(), propertyName);
                    if (property != null) validationRule = DisplayModelAttributesServices.GetValidationClass(property);
                }


                localSource.PropertyValue.ValidatesOnDataErrors = true;
                localSource.PropertyValue.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                if (validationRule != null) localSource.PropertyValue.ValidationRules.Add(validationRule);
            }


            if (localSource.ContentForVisualPresenter.Content?.GetType() == typeof(TextBox) && !localSource.Test)
            {
                TextBox? localContent = (localSource.ContentForVisualPresenter.Content as TextBox);
                
                if (localContent == null) return;

                localContent.SetBinding(TextBox.TextProperty, localSource.PropertyValue);
            }
        }

        // Header Text
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(name: "HeaderText", typeof(string), 
            typeof(PropertyDisplay), new PropertyMetadata(null));

        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        // Visual presenter type
        public static readonly DependencyProperty VisualPresenterTypeProperty = DependencyProperty.Register(name: "VisualPresenterType", typeof(VisualRepresenationType),
        typeof(PropertyDisplay), new FrameworkPropertyMetadata(defaultValue: VisualRepresenationType.Field,
            new PropertyChangedCallback(VisualPresenterTypeChangeCallback)));

        private static void VisualPresenterTypeChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PropertyDisplay? localSource = source as PropertyDisplay;
            if (localSource == null) return;

            localSource.SetContentForVisualPresenter((VisualRepresenationType)e.NewValue);
        }

        public VisualRepresenationType VisualPresenterType
        {
            get => (VisualRepresenationType)GetValue(VisualPresenterTypeProperty);
            set => SetValue(VisualPresenterTypeProperty, value);

        }
        #endregion


        #region Private methods
        private void SetContentForVisualPresenter(VisualRepresenationType type)
        {
            if (type == VisualRepresenationType.Field)
            {
                TextBox textBox = new();
                textBox.Style = this.FindResource("textBoxInError") as Style;
                ContentForVisualPresenter.Content = textBox;
                textBox.ToolTip


            }
        }
        #endregion
    }


}
