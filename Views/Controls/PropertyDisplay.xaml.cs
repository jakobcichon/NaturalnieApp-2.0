using NaturalnieApp2.Attributes;
using NaturalnieApp2.Interfaces;
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
            SetContentForVisualPresenter(ContentForVisualPresenter, VisualPresenterType);
        }

        #region Private fields
        List<string>? hintList = null;
        #endregion

        #region Events
        public class HasErorEventArgs: RoutedEventArgs
        {
            HasErorEventArgs(): base()
            {

            }

            public HasErorEventArgs(RoutedEvent routedEvent) : base(routedEvent: routedEvent)
            {

            }

            public bool? HasError { get; set; }
        }
        //public delegate void HasErrorChangedEventHandler(object sender, HasErorEventArgs e);
        //public event HasErrorChangedEventHandler HasErrorChangedEvent;

        // Register a custom routed event using the Bubble routing strategy.
        public static readonly RoutedEvent HasErrorChangedEvent = EventManager.RegisterRoutedEvent(
            name: "HasErrorChanged",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(PropertyDisplay));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler HasErrorChanged
        {
            add { AddHandler(HasErrorChangedEvent, value); }
            remove { RemoveHandler(HasErrorChangedEvent, value); }
        }
        #endregion



        #region Dependency properties

        public bool? HasError
        {
            get { return (bool?)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        // If field has error, this becomes true
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register("HasError", typeof(bool?), typeof(PropertyDisplay), 
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(HasErrorChangeCallback)));

        private static void HasErrorChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PropertyDisplay? localSource = source as PropertyDisplay;

           if (localSource == null) return;

            localSource.OnHasErrorChange();
        }

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
                AddValidationRule(localSource.PropertyValue);
            }

            ContentControl contentControl = localSource.ContentForVisualPresenter;

            object content = contentControl.Content;

            if (content == null) return;

            if (content.GetType() == typeof(TextBox))
            {
                localSource.BindPropertyToTextBox(contentControl);
                return;
            }

            if (content.GetType() == typeof(ComboBox))
            {
                localSource.BindPropertyToComboBox(contentControl);
                return;
            }
        }

        public List<string> HintItemsSource
        {
            get { return (List<string>)GetValue(HintItemsSourceProperty); }
            set { SetValue(HintItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HintItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintItemsSourceProperty =
            DependencyProperty.Register("HintItemsSource", typeof(List<string>), typeof(PropertyDisplay), new PropertyMetadata(null));


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

            localSource.SetContentForVisualPresenter(localSource.ContentForVisualPresenter, (VisualRepresenationType)e.NewValue);
        }

        public VisualRepresenationType VisualPresenterType
        {
            get => (VisualRepresenationType)GetValue(VisualPresenterTypeProperty);
            set => SetValue(VisualPresenterTypeProperty, value);

        }
        #endregion


        #region Private methods
        private static IHintListProvider? GetHintListProvider(Binding property)
        {
            IHintListProvider? hintListProvider = null;
            string? propertyName = property.Path?.Path.ToString();
            if (propertyName != null)
            {
                //Get validation class
                PropertyDescriptor? propertyDesc = DisplayModelAttributesServices.GetPropertyByName(property.Source.GetType(), propertyName);
                if (propertyDesc != null) hintListProvider = DisplayModelAttributesServices.GetHintListProvider(propertyDesc);
            }

            return hintListProvider;
        }
        private static void AddValidationRule(Binding property)
        {
            ValidationRule? validationRule = null;
            string? propertyName = property.Path?.Path.ToString();
            if (propertyName != null)
            {
                //Get validation class
                PropertyDescriptor? propertyDesc = DisplayModelAttributesServices.GetPropertyByName(property.Source.GetType(), propertyName);
                if (propertyDesc != null) validationRule = DisplayModelAttributesServices.GetValidationClass(propertyDesc);
            }


            property.ValidatesOnDataErrors = true;
            property.NotifyOnValidationError = true;
            property.NotifyOnSourceUpdated = true;

            property.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            if (validationRule != null) property.ValidationRules.Add(validationRule);
        }
        private void SetContentForVisualPresenter(ContentControl targetControl, VisualRepresenationType type)
        {
            switch (type)
            {
                case VisualRepresenationType.Field:
                    CreateTextBox(targetControl);
                    break;
                case VisualRepresenationType.List:
                    CreateComboBox(targetControl);
                    break;
            }
        }

        private void CreateTextBox(ContentControl targetControl)
        {
            TextBox textBox = new();

            // Cutomize error template
            Validation.SetErrorTemplate(textBox, null);

            // Customize appearance
            textBox.Style = (this.FindResource("SmallerFontStyle") as Style);

            // Add handlers
            Validation.AddErrorHandler(textBox, OnValidationError);
            textBox.SourceUpdated += TextBox_SourceUpdated;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;

            // Assigne object
            targetControl.Content = textBox;
        }

        private void CreateComboBox(ContentControl targetControl)
        {
            ComboBox comboBox = new ComboBox();

            // Customize appearance
            comboBox.Style = (this.FindResource("SmallerFontStyle") as Style);

            // Assigne object
            targetControl.Content = comboBox;
        }

        private void BindPropertyToTextBox(ContentControl contentControl)
        {

            TextBox? localContent = (contentControl.Content as TextBox);

            if (localContent == null) return;

            localContent.SetBinding(TextBox.TextProperty, PropertyValue);

            // Update source to force validation
            localContent.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void BindPropertyToComboBox(ContentControl contentControl)
        {
            ComboBox? localContent = (contentControl.Content as ComboBox);

            if (localContent == null) return;

            Binding binding = new Binding { Source = this, Path = new PropertyPath("HintItemsSource") };
            localContent.SetBinding(ComboBox.ItemsSourceProperty, binding);
            localContent.SetBinding(ComboBox.SelectedItemProperty, PropertyValue);


            HintItemsSource = GetHintListProvider(PropertyValue)?.GetData();

            // Update source to force validation
            /*            localContent.GetBindingExpression(TextBox.TextProperty).UpdateSource();*/


        }
        public void ShowBottomBar()
        {
            BottomBar.Visibility = Visibility.Visible;
        }

        public void HideBottomBar()
        {
            BottomBar.Visibility = Visibility.Collapsed;
        }

        public void ErroredBottomBarStyle()
        {
           BottomBar.Style = FindResource("ErroredBottomBarStyle") as Style;
        }

        public void RegularBottomBarStyle()
        {
            BottomBar.Style = FindResource("RegularBottomBarStyle") as Style;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            HideBottomBar();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ShowBottomBar();
        }

        private void TextBox_SourceUpdated(object? sender, DataTransferEventArgs e)
        {
            RemoveErrorIndicator();

            RegularBottomBarStyle();
        }

        private void OnValidationError(object? sender, ValidationErrorEventArgs e)
        {
            SetErrorIndicator(e.Error.ErrorContent);

            ErroredBottomBarStyle();

        }

        private void SetErrorIndicator(object errorContent)
        {
            ValidationErrorIndicator.Visibility = Visibility.Visible;

            var toolTip = new ToolTip();
            toolTip.Content = errorContent;
            ValidationErrorIndicator.ToolTip = toolTip;

            HasError = true;
        }


        private void RemoveErrorIndicator()
        {
            ValidationErrorIndicator.Visibility = Visibility.Collapsed;
            ValidationErrorIndicator.ToolTip = null;

            HasError = false;
        }

        private void OnHasErrorChange()
        {
            // Create a RoutedEventArgs instance.
            HasErorEventArgs routedEventArgs = new(routedEvent: HasErrorChangedEvent);
            routedEventArgs.HasError = HasError;

            // Raise the event, which will bubble up through the element tree.
            RaiseEvent(routedEventArgs);
        }


        #endregion

        private void PropertyDisplayUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnHasErrorChange();
        }
    }


}
