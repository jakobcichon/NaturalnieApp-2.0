using NaturalnieApp2.Interfaces.Services.Attributes;
using NaturalnieApp2.Views.Controls.Models;
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
using NaturalnieApp2.Services.Attributes;
using System.ComponentModel;
using NaturalnieApp2.Attributes;

namespace NaturalnieApp2.Views.Controls
{
    /// <summary>
    /// Interaction logic for ModelPropertyPresenter.xaml
    /// </summary>
    public partial class ModelPropertyPresenter : UserControl
    {
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
        public bool HasError2
        {
            get { return (bool)GetValue(HasError2Property); }
            set { SetValue(HasError2Property, value); }
        }

        // If field has error, this becomes true
        public static readonly DependencyProperty HasError2Property =
            DependencyProperty.Register("HasError2", typeof(bool), typeof(PropertyDisplay), new PropertyMetadata(false,
                new PropertyChangedCallback(HasError2ChangeCallback)));

        private static void HasError2ChangeCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
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
                    if (propertyToDisplay!= null) propertiesToDisplay.Add(propertyToDisplay);
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
            handler?.Invoke(this, new HasErorEventArgs { HasError = HasError2 });
        }
        #endregion

        private void PropertyDisplay_HasErrorChangedEvent(object sender, PropertyDisplay.HasErorEventArgs e)
        {
            SetValue(HasError2Property, e.HasError);
            OnHasErrorChange();
        }
    }
}
