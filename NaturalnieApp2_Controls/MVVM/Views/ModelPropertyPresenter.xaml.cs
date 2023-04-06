using NaturalnieApp2_Controls.Interfaces.Services.Attributes;
using NaturalnieApp2_Controls.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NaturalnieApp2_Controls.Services.Attributes;
using System.ComponentModel;
using NaturalnieApp2_Controls.Attributes;
using NaturalnieApp2_Controls.Services.VisualTreeHelperServices;

namespace NaturalnieApp2_Controls.Views.Controls
{
    /// <summary>
    /// Interaction logic for ModelPropertyPresenter.xaml
    /// </summary>
    public partial class ModelPropertyPresenter : UserControl
    {

        private Dictionary<PropertyDisplay, bool?> propetiesDictionary = new Dictionary<PropertyDisplay, bool?>();

        public ModelPropertyPresenter()
        {
            InitializeComponent();
        }


        #region Events
        public class HasErorEventArgs : EventArgs
        {
            public bool HasError { get; set; }
        }
        public delegate void HasErrorChangedEventHandler(object sender, HasErorEventArgs e);
        public event HasErrorChangedEventHandler HasErrorChangedEvent;
        #endregion

        #region Dependency properties
        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set 
            { 
                SetValue(HasErrorProperty, value); 
            }
        }

        // If field has error, this becomes true
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register("HasError", typeof(bool), typeof(ModelPropertyPresenter),
                                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(HasErrorChangeCallback)));

        private static void HasErrorChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {

            ModelPropertyPresenter? localSource = source as ModelPropertyPresenter;

            if (localSource == null) return;

            localSource.OnHasErrorChange();
        }

        public ObservableCollection<PropertyDisplayModel> PropertiesToDisplay
        {
            get { return (ObservableCollection<PropertyDisplayModel>)GetValue(PropertiesToDisplayProperty); }
            set { SetValue(PropertiesToDisplayProperty, value); }
        }

        // Properties to be displayed
        public static readonly DependencyProperty PropertiesToDisplayProperty = DependencyProperty.Register("PropertiesToDisplay",
                typeof(ObservableCollection<PropertyDisplayModel>),
                typeof(ModelPropertyPresenter),
                new PropertyMetadata(null));

        public IDisplayableModel? ModelToDisplay
        {
            get { return (IDisplayableModel)GetValue(ModelToDisplayProperty); }
            set { SetValue(ModelToDisplayProperty, value); }
        }

        public static readonly DependencyProperty ModelToDisplayProperty =
            DependencyProperty.Register("ModelToDisplay", typeof(IDisplayableModel), typeof(ModelPropertyPresenter),
                new PropertyMetadata(null, new PropertyChangedCallback(ModelToDisplaySourceChanged)));


        private static void ModelToDisplaySourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ModelPropertyPresenter)?.UpdatePropertiesToDisplay();
        }
        #endregion

        #region Private methods
        private void UpdatePropertiesToDisplay()
        {
            if (ModelToDisplay != null)
            {
                List<PropertyDescriptor> properties = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(ModelToDisplay.GetType());

                ObservableCollection<PropertyDisplayModel> propertiesToDisplay = new ObservableCollection<PropertyDisplayModel>();

                foreach (PropertyDescriptor property in properties)
                {
                    PropertyDisplayModel? propertyToDisplay = ExtractDataFromProperty(property, ModelToDisplay);
                    if (propertyToDisplay != null) propertiesToDisplay.Add(propertyToDisplay);
                }

                PropertiesToDisplay = propertiesToDisplay;
            }

        }

        private PropertyDisplayModel? ExtractDataFromProperty(PropertyDescriptor property, object instance)
        {
            if (!DisplayModelAttributesServices.CheckIfPropertyVisiable(property)) return null;

            PropertyDisplayModel model = new PropertyDisplayModel();

            // Get visual representation type
            VisualRepresenationType? type = DisplayModelAttributesServices.GetPropertyVisualRepresentationType(property);
            if (type != null) model.Type = type.Value;

            // Get header text
            string? text = DisplayModelAttributesServices.GetPropertyDisplayName(property);
            if (text != null) model.Header = text;

            // Get value
            Binding binding = new Binding(property.Name);
            binding.Source = instance;
            model.Value = binding;

            return model;

        }

        private void OnHasErrorChange()
        {
            HasErrorChangedEventHandler handler = HasErrorChangedEvent;
            handler?.Invoke(this, new HasErorEventArgs { HasError = HasError });
        }
        #endregion

        private bool CheckIfErrorExists()
        {
            if (propetiesDictionary.ContainsValue(true))
            {
                return true;
            }

            return false;
        }

        private void PropertyDisplay_HasErrorChanged(object sender, RoutedEventArgs e)
        {
            PropertyDisplay.HasErorEventArgs? localArgs = e as PropertyDisplay.HasErorEventArgs;
            PropertyDisplay? localSender = sender as PropertyDisplay;

            if (localArgs == null || localSender == null) return;

            if (!propetiesDictionary.ContainsKey(localSender))
            {
                propetiesDictionary.Add(localSender, localArgs.HasError);
            }
            else
            {
                propetiesDictionary[localSender] = localArgs.HasError;
            }

            if (CheckIfErrorExists())
            {
                HasError = true;
                OnHasErrorChange();
                return;
            }

            HasError = false;
            OnHasErrorChange();
            return;

        }
    }
 }
