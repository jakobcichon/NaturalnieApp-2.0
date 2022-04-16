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

        #region Dependency properties


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
        #endregion
    }
}
